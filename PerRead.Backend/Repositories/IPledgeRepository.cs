using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Commands;

namespace PerRead.Backend.Repositories
{
    public interface IPledgeRepository
    {
        Task<RequestPledge> CreatePledge(Author pledger, ArticleRequest request, PledgeCommand pledgeCommand);
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
    }
}

