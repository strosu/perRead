using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IPledgeService
    {
        Task<FEPledge> AddPledge(PledgeCommand pledgeCommand);
        Task<FERequest> RemovePledge(string pledgeId);
        Task<FEPledge> EditPledge(PledgeCommand pledgeComand);
        Task<FEPledge> GetPledge(string pledgeId);
    }

    public class PledgeService : IPledgeService
    {
        private readonly IPledgeRepository _pledgeRepository;
        private readonly IRequestsRepository _requestsRepository;
        private IRequesterGetter _requesterGetter;
        private readonly IAuthorRepository _authorRepository;

        public PledgeService(IPledgeRepository pledgeRepository, IRequestsRepository requestsRepository, IRequesterGetter requesterGetter, IAuthorRepository authorRepository)
        {
            _pledgeRepository = pledgeRepository;
            _requestsRepository = requestsRepository;
            _requesterGetter = requesterGetter;
            _authorRepository = authorRepository;
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
                await _requestsRepository.RemoveRequest(request);
                return null;
            }

            return request.ToFERequest(requester);
        }

        public async Task<FEPledge> EditPledge(PledgeCommand pledgeComand)
        {
            if (string.IsNullOrEmpty(pledgeComand.PledgeId))
            {
                throw new ArgumentException("You need an id to edit an existing pledge");
            }

            var pledge = await GetPledge(pledgeComand.PledgeId);

            if (pledge == null)
            {
                throw new ArgumentException($"Could not find the targeted pledge {pledgeComand.PledgeId}");
            }

            var requester = await _requesterGetter.GetRequester();
            return (await _pledgeRepository.UpdatePledge(pledgeComand)).ToFEPledge(requester);
        }

        public async Task<FEPledge> GetPledge(string pledgeId)
        {
            var requester = await _requesterGetter.GetRequester();
            return (await _pledgeRepository.GetPledge(pledgeId)
                .Include(x => x.ParentRequest)
                .ThenInclude(x => x.TargetAuthor).FirstOrDefaultAsync())?.ToFEPledge(requester);
        }
    }
}

