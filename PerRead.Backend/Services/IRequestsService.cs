using System;
using Microsoft.EntityFrameworkCore;
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
    }

    public class RequestsService : IRequestsService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IRequestsRepository _requestsRepository;
        private readonly IPledgeRepository _pledgeRepository;
        private IRequesterGetter _requesterGetter;

        public RequestsService(IAuthorRepository authorRepository, IRequestsRepository requestsRepository, IPledgeRepository pledgeRepository, IRequesterGetter requesterGetter)
        {
            _authorRepository = authorRepository;
            _requestsRepository = requestsRepository;
            _pledgeRepository = pledgeRepository;
            _requesterGetter = requesterGetter;
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
            await _authorRepository.MoveToEscrow(requester, pledgeCommand.TotalPledgeAmount);

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

            var pledgingUserCount = request.Pledges.Select(x => x.Pledger.AuthorId).Distinct().Count();

            if (pledgingUserCount > 1)
            {
                throw new ArgumentException("Other people have already pledged");
            }

            var requester = await _requesterGetter.GetRequester();

            return (await _requestsRepository.EditRequest(requestCommand)).ToFERequest(requester);
        }
    }
}

