using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Extensions;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IFeedsService
    {
        Task<Feed> CreateNewFeed(string feedName);

        Task<IEnumerable<FEArticlePreview>> GetFeedArticles(string feedId);

        Task AddAuthorToFeed(string feedId, string authorId);
    }

    public class FeedsService : IFeedsService
    {
        private readonly IFeedRepository _feedsRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IHttpContextAccessor _accessor;


        public FeedsService(IFeedRepository feedsRepository, IAuthorRepository authorRepository, IHttpContextAccessor accessor)
        {
            _feedsRepository = feedsRepository;
            _authorRepository = authorRepository;
            _accessor = accessor;
        }

        public async Task<Feed> CreateNewFeed(string feedName)
        {
            var currentUser = _accessor.GetUserId();
            var owner = await _authorRepository.GetAuthor(currentUser).FirstOrDefaultAsync();

            if (owner == null)
            {
                throw new ArgumentException("could not find the user for which to create the feed");
            }

            var feed = await _feedsRepository.CreateNewFeed(owner, feedName);

            return feed;
        }

        public async Task AddAuthorToFeed(string feedId, string authorId)
        {
            var author = await _authorRepository.GetAuthor(authorId).FirstOrDefaultAsync();

            if (author == null)
            {
                throw new ArgumentException("could not find the author to subscribe to");
            }

            await _feedsRepository.AddToFeed(feedId, author);
        }

        public async Task<IEnumerable<FEArticlePreview>> GetFeedArticles(string feedId)
        {
            var feedAuthors = _feedsRepository.GetAuthors(feedId);
            var articleAuthors = feedAuthors.Select(x => x.Articles).SelectMany(x => x);
            var articlesQuery = 
                articleAuthors.Select(x => x.Article)
                .OrderBy(x => x.CreatedAt).Take(20)
                .Select(x => x.ToFEArticlePreview());

            return await articlesQuery.ToListAsync();
        }
    }
}
