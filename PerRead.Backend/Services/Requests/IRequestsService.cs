using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Helpers.Errors;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.BusinessRules;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Models.Useful;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IRequestsService
    {
        Task<IEnumerable<FERequestPreview>> GetRequestsForAuthor(string authorId);

        Task<FERequest> GetRequest(string requestId);

        Task<FERequest> CreateRequest(CreateRequestCommand createRequestCommand);

        Task<FERequest> EditRequest(RequestCommand requestCommand);

        Task<FERequest> AcceptRequest(string requestId);

        Task<FERequest> CompleteRequest(CompleteRequestCommand completeRequestCommand);

        Task<FERequest> AbandonRequest(AbandonRequestCommand abandonRequestCommand);
    }

    public class RequestsService : IRequestsService
    {
        private readonly IWalletService _walletService;
        private readonly IAuthorRepository _authorRepository;
        private readonly IRequestsRepository _requestsRepository;
        private readonly IPledgeRepository _pledgeRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IRequesterGetter _requesterGetter;

        public RequestsService(IAuthorRepository authorRepository, IRequestsRepository requestsRepository, IPledgeRepository pledgeRepository, IRequesterGetter requesterGetter, IWalletService walletService, IArticleRepository articleRepository)
        {
            _authorRepository = authorRepository;
            _requestsRepository = requestsRepository;
            _pledgeRepository = pledgeRepository;
            _requesterGetter = requesterGetter;
            _walletService = walletService;
            _articleRepository = articleRepository;
        }

        public async Task<FERequest> CreateRequest(CreateRequestCommand createRequestCommand)
        {
            var requestCommand = createRequestCommand.RequestCommand;

            var requester = await _requesterGetter.GetRequester();

            var targetAuthor = await _authorRepository.GetAuthorAsync(createRequestCommand.TargetAuthorId);

            if (targetAuthor == null)
            {
                throw new NotFoundException("Could not find the target author");
            }

            var request = await _requestsRepository.CreateRequest(requester, targetAuthor, requestCommand);

            var pledgeCommand = createRequestCommand.PledgeCommand;

            await _pledgeRepository.CreatePledge(requester, request, pledgeCommand);
            await _walletService.MoveToEscrow(requester, pledgeCommand.TotalPledgeAmount);

            return await _requestsRepository.GetRequest(request.ArticleRequestId).Select(x => x.ToFERequest(requester)).SingleAsync();
        }

        public async Task<FERequest> GetRequest(string requestId)
        {
            var requester = await _requesterGetter.GetRequester();
            return await _requestsRepository.GetRequest(requestId).Select(x => x.ToFERequest(requester)).SingleAsync();
        }

        public async Task<IEnumerable<FERequestPreview>> GetRequestsForAuthor(string authorId)
        {
            var requester = await _requesterGetter.GetRequester();
            return await _requestsRepository.GetRequestsForAuthor(authorId).Select(x => x.ToFERequestPreview(requester)).ToListAsync();
        }

        public async Task<FERequest> EditRequest(RequestCommand requestCommand)
        {
            var request = await _requestsRepository.GetRequest(requestCommand.RequestId).FirstOrDefaultAsync();

            if (request == null)
            {
                throw new NotFoundException("Could not find the request");
            }

            if (request.RequestState != RequestState.Created)
            {
                throw new ConflictException("Request has already been accepted.");
            }

            var requester = await _requesterGetter.GetRequester();

            if (!request.IsEditableBy(requester))
            {
                throw new UnauthorizedException("Request is not editable by the current user.");
            }


            return (await _requestsRepository.EditRequest(requestCommand)).ToFERequest(requester);
        }

        public async Task<FERequest> AcceptRequest(string requestId)
        {
            var request = await ValidateUsersMatch(requestId);

            if (request.RequestState != RequestState.Created)
            {
                throw new ConflictException($"Only requests in the {nameof(RequestState.Created)} state can be accepted.");
            }

            await _requestsRepository.UpdateState(request, RequestState.Accepted);

            foreach (var pledge in request.Pledges)
            {
                await _walletService.ReleaseInitialPledgeFunds(pledge);
            }

            return request.ToFERequest(request.TargetAuthor);
        }

        public async Task<FERequest> AbandonRequest(AbandonRequestCommand abandonRequestCommand)
        {
            var request = await ValidateUsersMatch(abandonRequestCommand.RequestId);

            if (request.RequestState != RequestState.Accepted)
            {
                throw new ConflictException("You can only abandon requests that are active and accepted");
            }

            await _requestsRepository.UpdateState(request, RequestState.Cancelled);

            foreach (var pledge in request.Pledges)
            {
                await _walletService.ReleaseBackToPledger(pledge);
            }

            return request.ToFERequest(request.TargetAuthor);
        }

        public async Task<FERequest> CompleteRequest(CompleteRequestCommand completeRequestCommand)
        {
            var (request, resultingArticle) = await ValidateRequest(completeRequestCommand);

            await _requestsRepository.CompleteRequest(request, resultingArticle);
            await PostProcessing(request, resultingArticle);

            foreach (var pledge in request.Pledges)
            {
                await _walletService.ReleaseInitialPledgeFunds(pledge);
            }

            return request.ToFERequest(request.TargetAuthor);
        }

        private async Task PostProcessing(ArticleRequest request, Article resultingArticle)
        {
            if (request.PostPublishState == RequestPostPublishState.Public)
            {
                // If public, nothing to do here. The original author retains all the ownership and the article is visible
                return;
            }

            if (request.PostPublishState == RequestPostPublishState.Exclusive)
            {
                // Mark the article as exclusive. The rest should be handled via the same route as ProfitShare (which just sets the ownership percentages)
                await _articleRepository.MarkAsOwnersOnly(resultingArticle);
            }

            // The ownership needs to be computed for both ProfitShare and Exclusive (we're using a 100% ownership for exclusive)
            var ownerships = await ComputeOwnership(request, resultingArticle);
            await _articleRepository.UpdateOwners(resultingArticle, ownerships);

            return;


        }

        private async Task<IEnumerable<AuthorOwnership>> ComputeOwnership(ArticleRequest request, Article resultingArticle)
        {
            if (resultingArticle.AuthorsLink.Count != 1)
            {
                throw new ConflictException("This should have been validated earlier");
            }

            double percentageForPledgersNormalized = (double)request.PercentForledgers / 100;
            double leftoverSum = 1;

            var result = new List<AuthorOwnership>();

            // At this point we're interested in the total amount pledged, as the request is about to be completed.
            // The total amount is what gives the final ownership, the initial pledge amount has no impact here
            var pledgedSum = request.Pledges.Sum(x => x.TotalTokenSum);

            var pledgers = request.Pledges.GroupBy(x => x.Pledger).Select(group => new { Author = group.Key, TotalPledged = group.Sum(x => x.TotalTokenSum) });
            foreach (var pledger in pledgers)
            {
                double percentageOfPledgers = (double)pledger.TotalPledged / pledgedSum;
                var currentOwnership = percentageForPledgersNormalized * percentageOfPledgers;
                leftoverSum -= currentOwnership;

                result.Add(new AuthorOwnership
                {
                    Author = pledger.Author,
                    Ownership = currentOwnership,
                    CanBeEdited = false,
                    IsUserFacing = false
                });
            }

            result.Add(new AuthorOwnership 
            {
                Author = resultingArticle.AuthorsLink.First().Author,
                Ownership = leftoverSum
            });

            return result;
        }

        private async Task<ArticleRequest> ValidateUsersMatch(string requestId)
        {
            var request = await _requestsRepository.GetRequest(requestId).FirstOrDefaultAsync();

            if (request == null)
            {
                throw new NotFoundException("Could not find the request");
            }

            var currentUser = await _requesterGetter.GetRequester();

            if (request.TargetAuthor != currentUser)
            {
                throw new UnauthorizedException("You can only act on requests where you are the target author");
            }

            return request;
        }

        private async Task<(ArticleRequest request, Article resultingArticle)> ValidateRequest(CompleteRequestCommand completeRequestCommand)
        {
            var request = await ValidateUsersMatch(completeRequestCommand.RequestId);

            var resultingArticle = await _articleRepository.GetWithOwnersInternal(completeRequestCommand.ResultingArticleId, true).SingleAsync(); // The target article should never be exclusive at this point

            if (resultingArticle == null)
            {
                throw new NotFoundException("Invalid article ID");
            }

            if (request.RequestState != RequestState.Accepted)
            {
                throw new ConflictException("A request can only be accepted if it's state is Created.");
            }

            var users = resultingArticle.AuthorsLink.Select(x => x.AuthorId).ToList();

            var currentUser = await _requesterGetter.GetRequester();

            if (users.Count != 1 || users[0] != currentUser.AuthorId)
            {
                throw new UnauthorizedException("You need to be the only author of the target article");
            }

            return (request, resultingArticle);
        }
    }
}

