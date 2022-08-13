using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<FEArticlePreview>> GetAll();

        Task<FEArticle?> Get(string id);

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
                throw new ArgumentException("Only existing users are allowed to create articles");
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

        public async Task<IEnumerable<FEArticlePreview>> GetAll()
        {
            var articles = _articleRepository.GetAll().OrderByDescending(a => a.CreatedAt);

            var requester = await _requesterGetter.GetRequesterWithArticles();

            return await articles.Select(article => article.ToFEArticlePreview(requester)).ToListAsync();
        }

        public async Task<FEArticle?> Get(string id)
        {
            var article = await _articleRepository.Get(id);
            return article?.ToFEArticle();
        }

        public async Task Delete(string id)
        {
            var article = await _articleRepository.Get(id);

            if (article == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await _articleRepository.Delete(id);
        }

        public async Task<TransactionResult> UnlockForCurrentUser(string id)
        {
            // TODO - might want to move this, no idea where atm
            var article = await _articleRepository.GetWithOwners(id);
            var requester = await _requesterGetter.GetRequesterWithArticles();

            if (IsAlreadyUnlocked(requester, article))
            {
                // We don't need to add it anywhere
                return TransactionResult.Success;
            }


            return await PayOwners(requester, article);

            //var price = ComputeArticleCost(requester, article);
            //return await _walletService.UnlockArticle(article.ArticleAuthors.First().Author, price);
        }

        private bool IsAlreadyUnlocked(Author requester, Article article)
        {
            if (requester.UnlockedArticles.Any(x => x.ArticleId == article.ArticleId))
            {
                return true;
            }

            return false;
        }

        private long ComputeArticleCost(Author requester, Article article)
        {
            if (article.ArticleOwners.Any(x => x.AuthorId == requester.AuthorId))
            {
                return 0;
            }

            return article.Price;
        }

        private async Task<TransactionResult> PayOwners(Author requester, Article article)
        {
            // If you already have an ownership stake in the article, you're not charged for reading it
            if (article.ArticleOwners.Any(x => x.AuthorId == requester.AuthorId))
            {
                return TransactionResult.Success;
            }

            // TODO - this needs to be a single transaction for the consumer, while still splitting the momey between different authors.
            // Either transact once from the reader and then split somehow (intermediate company wallet?)
            // Or do as is, but somehow group the transactions when returning to the user
            var paymentTasks = new List<Task<TransactionResult>>();
            foreach (var owner in article.ArticleOwners)
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
                paymentTasks.Add(_walletService.UnlockArticle(owner.Author, uIntAmount));
            }

            var listResult = await Task.WhenAll(paymentTasks);

            var firstFailed = listResult.FirstOrDefault(x => x.Result == PaymentResultEnum.Failed);

            if (firstFailed != null)
            {
                return firstFailed;
            }

            return TransactionResult.Success;
        }

        public async Task<FEArticleOwnership> GetOwnership(string id)
        {
            var article = await _articleRepository.Get(id);
            var requester = await _requesterGetter.GetRequester();

            if (!article.ArticleOwners.Any(x => x.AuthorId == requester.AuthorId))
            {
                throw new ArgumentException("You're not an owner");
            }

            return article.ToFEArticleOwnership();
        }

        // TODO - probably take another type here
        public async Task<FEArticleOwnership> SetOwnership(string id, UpdateOwnershipCommand ownershipCommand)
        {
            var currentArticle = await _articleRepository.GetWithOwners(id);

            if (ownershipCommand.Owners.GroupBy(x => x.AuthorId).Any(x => x.Count() > 1))
            {
                throw new ArgumentException("An author can only appear once");
            }

            // Firat, check that none of the required authors were edited
            var notEditableList = currentArticle.ArticleOwners.Where(x => !x.CanBeEdited);
            foreach (var notEditable in notEditableList)
            {
                var updated = ownershipCommand.Owners.FirstOrDefault(x => x.AuthorId == notEditable.AuthorId);

                if (updated == null)
                {
                    throw new ArgumentException($"You removed author {updated.AuthorId} which cannot be edited");
                }

                if (updated.OwnershipPercent != notEditable.OwningPercentage * 100)
                {
                    throw new ArgumentException($"You cannot edit user {updated.AuthorId}'s percentage");
                }
            }

            // Then compute what's available once the required users take their stake
            var reserved = currentArticle.ArticleOwners.Sum(x => x.OwningPercentage) * 100;
            if (reserved > 100)
            {
                throw new ArgumentException("Somehow we ended up with more than 100% ownership, better call someone");
            }

            var available = 100 - reserved;
            var requetedEditable = ownershipCommand.Owners.Where(x => !notEditableList.Any(y => y.AuthorId == x.AuthorId));
            var requtedEditableSum = requetedEditable.Sum(x => x.OwnershipPercent);
            if (available != requtedEditableSum)
            {
                throw new ArgumentException($"There is {available} available, but you requeted {requetedEditable}");
            }

            await _articleRepository.UpdateOwners(id, ownershipCommand);

            return currentArticle.ToFEArticleOwnership();
        }
    }
}

