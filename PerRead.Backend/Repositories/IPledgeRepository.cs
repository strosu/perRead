using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Commands;

namespace PerRead.Backend.Repositories
{
    public interface IPledgeRepository
    {
        Task<RequestPledge> CreatePledge(Author pledger, ArticleRequest request, PledgeCommand pledgeCommand);

        Task<RequestPledge> UpdatePledge(PledgeCommand pledgeCommand);

        IQueryable<RequestPledge> GetPledge(string pledgeId);

        Task DeletePledge(RequestPledge pledge);
    }

    public class PledgeRepository : IPledgeRepository
    {
        private readonly AppDbContext _context;

        public PledgeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RequestPledge> CreatePledge(Author pledger, ArticleRequest request, PledgeCommand pledgeCommand)
        {
            var pledge = new RequestPledge
            {
                RequestPledgeId = Guid.NewGuid().ToString(),
                ParentRequest = request,
                Pledger = pledger,
                TokensOnAccept = pledgeCommand.UpfrontPledgeAmount,
                TotalTokenSum = pledgeCommand.TotalPledgeAmount,
                CreatedAt = DateTime.UtcNow
            };

            _context.Pledges.Add(pledge);
            await _context.SaveChangesAsync();

            return pledge;
        }

        public async Task DeletePledge(RequestPledge pledge)
        {
            _context.Pledges.Remove(pledge);
            await _context.SaveChangesAsync();
        }

        public IQueryable<RequestPledge> GetPledge(string pledgeId)
        {
            return _context.Pledges.Where(x => x.RequestPledgeId == pledgeId);
        }

        public Task<RequestPledge> UpdatePledge(PledgeCommand pledgeCommand)
        {
            throw new NotImplementedException();
        }
    }
}

