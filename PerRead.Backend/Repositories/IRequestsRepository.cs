using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Repositories.Extensions;

namespace PerRead.Backend.Repositories
{
    public interface IRequestsRepository
    {
        IQueryable<ArticleRequest> GetRequestsForAuthor(string authorId);
        IQueryable<ArticleRequest> GetRequest(string requestId);
        Task<ArticleRequest> CreateRequest(Author initiator, Author targetAuthor, RequestCommand requestCommand);
    }

    public class RequestsRepository : IRequestsRepository
    {
        private readonly AppDbContext _context;

        public RequestsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ArticleRequest> CreateRequest(Author initiator, Author targetAuthor, RequestCommand requestCommand)
        {
            var request = new ArticleRequest
            {
                ArticleRequestId = Guid.NewGuid().ToString(),
                TargetAuthor = targetAuthor,
                Initiator = initiator,
                Title = requestCommand.Title,
                Description = requestCommand.Description,
                Deadline = requestCommand.Deadline,
                PercentForledgers = requestCommand.PercentForPledgers,
                PostPublishState = requestCommand.PostPublishState,
                RequestState = RequestState.Created,
                Pledges = new List<RequestPledge>(),
                CreatedAt = DateTime.UtcNow
            };

            _context.Requests.Add(request);

            await _context.SaveChangesAsync();
            return request;
        }

        public IQueryable<ArticleRequest> GetRequest(string requestId)
        {
            return _context.Requests.Where(x => x.ArticleRequestId == requestId)
                .WithPledges()
                .WithTargetAuthor();
        }

        public IQueryable<ArticleRequest> GetRequestsForAuthor(string authorId)
        {
            return _context.Requests.Where(x => x.TargetAuthor.AuthorId == authorId)
                .WithTargetAuthor()
                .WithPledges();
        }
    }
}

