using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.BusinessRules;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
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
        private IRequesterGetter _requesterGetter;

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
                throw new ArgumentException("Could not find the target author");
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
                throw new ArgumentException("Could not find the request");
            }

            if (request.RequestState != RequestState.Created)
            {
                throw new ArgumentException("Request has already been accepted.");
            }

            var requester = await _requesterGetter.GetRequester();

            if (!RequestRules.IsEditable(request, requester))
            {
                throw new ArgumentException("Request is not editable by the current user.");
            }


            return (await _requestsRepository.EditRequest(requestCommand)).ToFERequest(requester);
        }

        public async Task<FERequest> AcceptRequest(string requestId)
        {
            var request = await ValidateUsersMatch(requestId);

            if (request.RequestState != RequestState.Created)
            {
                throw new ArgumentException("Request cannot be accepted");
            }

            await _requestsRepository.UpdateState(request, RequestState.Accepted);

            foreach (var pledge in request.Pledges)
            {
                await _walletService.ReleaseInitialPledgeFunds(pledge);
            }

            return request.ToFERequest(request.TargetAuthor);
        }

        public async Task<FERequest> CompleteRequest(CompleteRequestCommand completeRequestCommand)
        {
            var request = await ValidateUsersMatch(completeRequestCommand.RequestId);

            if (request.RequestState != RequestState.Accepted)
            {
                throw new ArgumentException("A request can only be accepted if it's state is Created.");
            }

            var resultingArticle = await _articleRepository.GetSimpleArticle(completeRequestCommand.ResultingArticleId);
            ;
            if (resultingArticle == null)
            {
                throw new ArgumentException("Invalid article ID");
            }

            await _requestsRepository.CompleteRequest(request, resultingArticle);

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
                throw new ArgumentException("You can only abandon requests that are active and accepted");
            }

            await _requestsRepository.UpdateState(request, RequestState.Cancelled);

            foreach (var pledge in request.Pledges)
            {
                await _walletService.ReleaseBackToPledger(pledge);
            }

            return request.ToFERequest(request.TargetAuthor);
        }

        private async Task<ArticleRequest> ValidateUsersMatch(string requestId)
        {
            var request = await _requestsRepository.GetRequest(requestId).FirstOrDefaultAsync();

            if (request == null)
            {
                throw new ArgumentException("Could not find the request");
            }

            var currentUser = await _requesterGetter.GetRequester();

            if (request.TargetAuthor != currentUser)
            {
                throw new ArgumentException("You can only accept requests where you are the target author");
            }

            return request;
        }
    }
}

