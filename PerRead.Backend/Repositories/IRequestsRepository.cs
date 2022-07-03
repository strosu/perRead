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
        Task<ArticleRequest> EditRequest(RequestCommand requestCommand);
        Task RemoveRequest(ArticleRequest request);
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

        public async Task<ArticleRequest> EditRequest(RequestCommand requestCommand)
        {
            var request = await _context.Requests.Where(x => x.ArticleRequestId == requestCommand.RequestId).FirstOrDefaultAsync();

            request.Title = requestCommand.Title;
            request.Description = requestCommand.Description;
            request.Deadline = requestCommand.Deadline;
            request.PostPublishState = requestCommand.PostPublishState;
            request.PercentForledgers = requestCommand.PercentForPledgers;

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

        public async Task RemoveRequest(ArticleRequest request)
        {
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }
}

