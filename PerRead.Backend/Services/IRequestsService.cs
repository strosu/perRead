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
            var requestCommand = createRequestCommand.Request;

            var requester = await _requesterGetter.GetRequester();

            var targetAuthor = await _authorRepository.GetAuthorAsync(requestCommand.TargetAuthorId);

            if (targetAuthor == null)
            {
                throw new ArgumentException("Could not find the target author");
            }

            var request = await _requestsRepository.CreateRequest(requester, targetAuthor, requestCommand);

            var pledgeCommand = createRequestCommand.Pledge;

            await _pledgeRepository.CreatePledge(requester, request, pledgeCommand);

            return await _requestsRepository.GetRequest(request.ArticleRequestId).Select(x => x.ToFERequest()).SingleAsync();
        }

        public async Task<FERequest> GetRequest(string requestId)
        {
            return await _requestsRepository.GetRequest(requestId).Select(x => x.ToFERequest()).SingleAsync();
        }

        public async Task<IEnumerable<FERequestPreview>> GetRequestsForAuthor(string authorId)
        {
            var requester = await _requesterGetter.GetRequester();
            return await _requestsRepository.GetRequestsForAuthor(authorId).Select(x => x.ToFERequestPreview(requester)).ToListAsync();
        }

    }
}

