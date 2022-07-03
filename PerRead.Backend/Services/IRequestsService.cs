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
        Task<FEPledge> AddPledge(PledgeCommand pledgeCommand);
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

        public async Task<FEPledge> AddPledge(PledgeCommand pledgeCommand)
        {
            var request = await _requestsRepository.GetRequest(pledgeCommand.RequestId).FirstOrDefaultAsync();

            if (request == null)
            {
                throw new ArgumentException("Request does not exist");
            }

            var requester = await _requesterGetter.GetRequester();
            var pledge = await _pledgeRepository.CreatePledge(requester, request, pledgeCommand);
            await _authorRepository.MoveToEscrow(requester, pledgeCommand.TotalPledgeAmount);
            return pledge.ToFEPledge(requester);
        }

        public async Task<FERequest> RemovePledge(string pledgeId)
        {
            var pledge = await _pledgeRepository.GetPledge(pledgeId)
                .Include(x => x.ParentRequest).FirstOrDefaultAsync();

            if (pledge == null)
            {
                throw new ArgumentException("Pledge does not exist");
            }

            var requester = await _requesterGetter.GetRequester();
            if (pledge.Pledger.AuthorId != requester.AuthorId)
            {
                throw new ArgumentException("You don't own this, and thus cannot delete it");
            }

            await _pledgeRepository.DeletePledge(pledge);

            var request = await _requestsRepository.GetRequest(pledge.ParentRequest.ArticleRequestId).FirstOrDefaultAsync();

            if (request.Pledges.Count == 1)
            {
                //await _requestsRepository.Delete(request);
                return null;
            }

            return request.ToFERequest(requester);
        }

        public async Task<FERequest> CreateRequest(CreateRequestCommand createRequestCommand)
        {
            var requestCommand = createRequestCommand.RequestCommand;

            var requester = await _requesterGetter.GetRequester();

            var targetAuthor = await _authorRepository.GetAuthorAsync(requestCommand.TargetAuthorId);

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

    }
}

