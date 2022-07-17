using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;
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
        private readonly IWalletService _walletService;
        private readonly IPledgeRepository _pledgeRepository;
        private readonly IRequestsRepository _requestsRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IRequesterGetter _requesterGetter;

        public PledgeService(IPledgeRepository pledgeRepository, IRequestsRepository requestsRepository, IRequesterGetter requesterGetter, IAuthorRepository authorRepository, IWalletService walletService)
        {
            _pledgeRepository = pledgeRepository;
            _requestsRepository = requestsRepository;
            _requesterGetter = requesterGetter;
            _authorRepository = authorRepository;
            _walletService = walletService;
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
            await _walletService.MoveToEscrow(requester, pledgeCommand.TotalPledgeAmount);
            return pledge.ToFEPledge(requester);
        }

        public async Task<FERequest> RemovePledge(string pledgeId)
        {
            var pledge = await _pledgeRepository.GetPledge(pledgeId)
                .Include(x => x.ParentRequest)
                .FirstOrDefaultAsync();

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
            await _walletService.MoveFromEscrow(requester, pledge.TotalTokenSum);

            var request = await _requestsRepository.GetRequest(pledge.ParentRequest.ArticleRequestId).FirstOrDefaultAsync();

            if (request.Pledges.Count == 0)
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

            var pledge = await GetPledgeObject(pledgeComand.PledgeId);

            if (pledge == null)
            {
                throw new ArgumentException($"Could not find the targeted pledge {pledgeComand.PledgeId}");
            }

            var initialAmount = pledge.TotalTokenSum;

            var newPledge = await _pledgeRepository.UpdatePledge(pledgeComand);

            if (newPledge.TotalTokenSum > initialAmount)
            {
                await _walletService.MoveToEscrow(pledge.Pledger, newPledge.TotalTokenSum - initialAmount);
            }
            else
            {
                await _walletService.MoveFromEscrow(pledge.Pledger, initialAmount - newPledge.TotalTokenSum);
            }

            var requester = await _requesterGetter.GetRequester();
            return newPledge.ToFEPledge(requester);
        }

        public async Task<FEPledge> GetPledge(string pledgeId)
        {
            var requester = await _requesterGetter.GetRequester();
            return (await GetPledgeObject(pledgeId)).ToFEPledge(requester);
        }

        private async Task<RequestPledge> GetPledgeObject(string pledgeId)
        {
            return (await _pledgeRepository.GetPledge(pledgeId)
                .Include(x => x.ParentRequest)
                .ThenInclude(x => x.TargetAuthor)
                .Include(x => x.Pledger).FirstOrDefaultAsync());
        }
    }
}

