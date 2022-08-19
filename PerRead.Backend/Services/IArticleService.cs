using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Helpers;
using PerRead.Backend.Helpers.Errors;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Models.Useful;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<FEArticlePreview>> GetAllVisible();

        Task<FEArticle?> GetIfVisible(string id);

        Task<FEArticle> Create(CreateArticleCommand article);

        Task Delete(string id);

        Task<TransactionResult> UnlockForCurrentUser(string id);

        Task<FEArticleOwnership> GetOwnership(string id);

        Task<FEArticleOwnership> SetOwnership(string id, UpdateOwnershipCommand ownership);
    }

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ITagRepository _tagRespository;
        private readonly IImageService _imageService;
        private readonly ISectionRepository _sectionRepository;
        private readonly IRequesterGetter _requesterGetter;
        private readonly IWalletService _walletService;

        public ArticleService(
            IArticleRepository articleRepository,
            IAuthorRepository authorRepository,
            ITagRepository tagRespository,
            IImageService imageService,
            ISectionRepository sectionRepository,
            IRequesterGetter requesterGetter,
            IWalletService walletService)
        {
            _articleRepository = articleRepository;
            _authorRepository = authorRepository;
            _tagRespository = tagRespository;
            _imageService = imageService;
            _sectionRepository = sectionRepository;
            _requesterGetter = requesterGetter;
            _walletService = walletService;
        }

        public async Task<FEArticle> Create(CreateArticleCommand article)
        {
            article.CheckValid();

            var author = await _requesterGetter.GetRequester();

            if (author == null)
            {
                throw new UnauthorizedException("Only existing users are allowed to create articles");
            }

            // Ensure the tags are created
            var tagTasks = article.Tags.Select(tag => _tagRespository.GetOrCreate(tag)).ToList();
            await Task.WhenAll(tagTasks);

            // Save the image somewhere usefull and get its path
            //var path = await _imageService.Save(author.AuthorId, article.ArticleImage);
            var path = "";

            var sections = await _sectionRepository.GetAllSections().Where(x => article.SectionIds.Contains(x.SectionId)).ToListAsync();

            // Create the article itself
            var articleModel = await _articleRepository.Create(author, tagTasks.Select(tagTask => tagTask.Result), sections, path, article);

            await _authorRepository.IncrementPublishedArticleCount(author.AuthorId);

            return articleModel.ToFEArticle();
        }

        public async Task<IEnumerable<FEArticlePreview>> GetAllVisible()
        {
            var requester = await _requesterGetter.GetRequesterWithArticles();
            var articles = _articleRepository.GetAllVisible(requester.AuthorId).OrderByDescending(a => a.CreatedAt);

            return await articles.Select(article => article.ToFEArticlePreview(requester)).ToListAsync();
        }

        public async Task<FEArticle?> GetIfVisible(string id)
        {
            var requester = await _requesterGetter.GetRequesterWithArticles();
            var article = await _articleRepository.GetVisible(id, requester.AuthorId).FirstOrDefaultAsync();

            if (article == null)
            {
                throw new NotFoundException($"Article id {id} does not exist");
            }
            return article?.ToFEArticle();
        }

        public async Task Delete(string id)
        {
            var article = await _articleRepository.GetInternal(id).SingleAsync();

            if (article == null)
            {
                throw new NotFoundException($"Article id {id} does not exist");
            }

            await _articleRepository.Delete(id);
        }

        public async Task<TransactionResult> UnlockForCurrentUser(string id)
        {
            // TODO - might want to move this, no idea where atm
            var requester = await _requesterGetter.GetRequesterWithArticles();
            var article = await _articleRepository.GetInternal(id).SingleAsync(); // Unlocking will never apply to an exclusive article

            if (requester.HasUnlockedArticle(article))
            {
                // We don't need to add it anywhere
                return TransactionResult.Success;
            }

            if (requester.Owns(article))
            {
                // If owned (e.g. via a request), mark that it is now read and don't charge anyt
                await _authorRepository.MarkAsRead(requester, article);
                return TransactionResult.Success;
            }

            var result = await PayOwners(requester, article);
            
            if (result.Result == PaymentResultEnum.Success)
            {
                await _authorRepository.MarkAsRead(requester, article);
            }

            return result;
        }

        public async Task<FEArticleOwnership> GetOwnership(string id)
        {
            var requester = await _requesterGetter.GetRequester();
            var article = await _articleRepository.GetVisible(id, requester.AuthorId).SingleAsync();

            if (!article.AuthorsLink.Any(x => x.AuthorId == requester.AuthorId))
            {
                throw new UnauthorizedException("You're not an owner");
            }

            return article.ToFEArticleOwnership();
        }

        // TODO - probably take another type here
        public async Task<FEArticleOwnership> SetOwnership(string id, UpdateOwnershipCommand ownershipCommand)
        {
            var authors = await ValidateOwnersCommand(ownershipCommand);

            var currentArticle = await _articleRepository.GetInternal(id,  true).SingleAsync(); // Setting ownership will never apply to an exclusive article

            // Firat, check that none of the required authors were edited
            var notEditableList = currentArticle.AuthorsLink.Where(x => !x.CanBeEdited);
            foreach (var notEditable in notEditableList)
            {
                var updated = ownershipCommand.Owners.FirstOrDefault(x => x.AuthorId == notEditable.AuthorId);

                if (updated == null)
                {
                    throw new MalformedDataException($"You removed author {updated.AuthorId} which cannot be edited");
                }

                if (updated.OwnershipPercent != notEditable.OwningPercentage * 100)
                {
                    throw new MalformedDataException($"You cannot edit user {updated.AuthorId}'s percentage");
                }
            }

            // Then compute what's available once the required users take their stake
            var reserved = notEditableList.Sum(x => x.OwningPercentage) * 100;
            if (reserved > 100)
            {
                throw new MalformedDataException("Somehow we ended up with more than 100% ownership, better call someone");
            }

            var available = 100 - reserved;
            var requetedEditable = ownershipCommand.Owners.Where(x => !notEditableList.Any(y => y.AuthorId == x.AuthorId));
            var requtedEditableSum = requetedEditable.Sum(x => x.OwnershipPercent);
            if (available != requtedEditableSum)
            {
                throw new MalformedDataException($"There is {available} available, but you requeted {requtedEditableSum}");
            }

            var authorOwnerships = ownershipCommand.Owners.Select(x => new AuthorOwnership
            {
                Author = authors.First(y => y.AuthorId == x.AuthorId),
                Ownership = x.OwnershipPercent / 100,
                CanBeEdited = true, // TODO - this relies on the underlying implementation in UpdateOwners (i.e. these values are only used for new entries, which happes to make them correct), needs a cleaner way to do it
                IsUserFacing = true
            });
            
            return (await _articleRepository.UpdateOwners(currentArticle, authorOwnerships)).ToFEArticleOwnership();
        }

        private async Task<IEnumerable<Author>> ValidateOwnersCommand(UpdateOwnershipCommand command)
        {
            if (command.Owners.GroupBy(x => x.AuthorId).Any(x => x.Count() > 1))
            {
                throw new MalformedDataException("An author can only appear once");
            }


            foreach (var owner in command.Owners)
            {
                if (owner.OwnershipPercent > 100)
                {
                    throw new MalformedDataException("An ownership percentage cannot exceed 100");
                }
            }

            var requestedAuthorIds = command.Owners.Select(x => x.AuthorId);

            var existingAuthors = await _authorRepository.GetAuthors()
                .Where(x => requestedAuthorIds.Contains(x.AuthorId))
                .Include(x => x.ArticlesLink).ToListAsync();

            var nonExistingAuthorIds = requestedAuthorIds.Where(x => !existingAuthors.Any(y => x == y.AuthorId));
            if (nonExistingAuthorIds.Any())
            {
                throw new NotFoundException($"Could not find the following authorIds: {string.Join(' ', nonExistingAuthorIds)}");
            }

            return existingAuthors;
        }



        private async Task<TransactionResult> PayOwners(Author requester, Article article)
        {
            // If you already have an ownership stake in the article, you're not charged for reading it
            if (article.AuthorsLink.Any(x => x.AuthorId == requester.AuthorId))
            {
                return TransactionResult.Success;
            }

            var firstLeg = await _walletService.TransferArticlePriceToCompany(requester, article.Price, article.ArticleId);

            if (firstLeg.Result != PaymentResultEnum.Success)
            {
                return firstLeg;
            }

            return await PayAuthors(article);
        }

        private async Task<TransactionResult> PayAuthors(Article article)
        {
            var paymentTasks = new List<Task<TransactionResult>>();
            uint sumToBePaid = 0;
            foreach (var owner in article.AuthorsLink.Where(x => !x.IsPublisher))
            {
                var amount = article.Price * owner.OwningPercentage;

                // TODO - need a better algorithm for splitting the money; probably a round robin kind of deal between owners if they wouldn't all get something out of every read
                // E.g. you own 1% of an article, but its price is 5, you should only get 1 token every 2 reads.
                // Need to also persist the "latest head" of the RR
                // For now, just do some basic stuff for demo purposes
                if (amount < 1)
                {
                    continue;
                }

                var uIntAmount = (uint)amount;
                sumToBePaid += uIntAmount;
                paymentTasks.Add(_walletService.CompanyTransferToAuthor(owner.Author, uIntAmount, article.ArticleId));
            }

            // Add the final task, to the original publisher. We do this at the end in order to round up the value
            var publisher = article.AuthorsLink.FirstOrDefault(x => x.IsPublisher);
            if (publisher != null)
            {
                paymentTasks.Add(_walletService.CompanyTransferToAuthor(publisher.Author, article.Price - sumToBePaid, article.ArticleId));
            }

            var listResult = await Task.WhenAll(paymentTasks);

            var firstFailed = listResult.FirstOrDefault(x => x.Result == PaymentResultEnum.Failed);

            if (firstFailed != null)
            {
                return firstFailed;
            }

            return TransactionResult.Success;
        }
    }
}

