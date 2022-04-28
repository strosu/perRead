using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Repositories
{
    public interface IFeedRepository
    {
        Task<Feed> CreateNewFeed(Author owner, string name);

        Task AddToFeed(string feedId, Author authorToAdd);

        Task<Feed> GetFeedInfo(string feedId);

        Task<Feed> GetFeedWithAuthors(string feedId);

        IQueryable<Feed> GetUserFeeds(Author owner);

        IQueryable<Author> GetAuthors(string feedId);

        Task UpdateFeed(Feed feed);

        Task DeleteFeed(Feed feed);
    }

    public class FeedRepository : IFeedRepository
    {
        private readonly AppDbContext _context;

        public FeedRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Feed> CreateNewFeed(Author owner, string name)
        {
            var feed = new Feed
            {
                FeedName = name,
                Owner = owner,
                FeedId = Guid.NewGuid().ToString(),
                RequireConfirmationAbove = 1,
                ShowFreeArticles = true,
                ShowArticlesAboveConfirmationLimit = true,
                ShowUnaffordableArticles = true,
            };

            _context.Feeds.Add(feed);

            await _context.SaveChangesAsync();

            return feed;
        }

        public async Task<Feed> GetFeedInfo(string feedId)
        {
            return await _context.Feeds
                .Where(x => x.FeedId == feedId)
                .Include(x => x.Owner).FirstOrDefaultAsync();
        }

        public async Task<Feed> GetFeedWithAuthors(string feedId)
        {
            return await _context.Feeds
                .Where(x => x.FeedId == feedId)
                .Include(x => x.SubscribedAuthors).FirstOrDefaultAsync();
        }

        public async Task AddToFeed(string feedId, Author authorToAdd)
        {
            var feed = await _context.Feeds.FindAsync(feedId);

            if (feed == null)
            {
                throw new ArgumentException("feed does not exist");
            }

            if (feed.SubscribedAuthors == null)
            {
                feed.SubscribedAuthors = new List<Author>();
            }

            feed.SubscribedAuthors.Add(authorToAdd);

            await _context.SaveChangesAsync();
        }

        public IQueryable<Author> GetAuthors(string feedId)
        {
            return _context.Feeds
                .AsNoTracking().Where(x => x.FeedId == feedId)
                .Include(x => x.SubscribedAuthors)
                .ThenInclude(x => x.Sections)
                .ThenInclude(x => x.Articles)
                .ThenInclude(x => x.Article)
                .ThenInclude(x => x.ArticleAuthors)
                .ThenInclude(x => x.Author)
                .SelectMany(f => f.SubscribedAuthors);
        }

        public IQueryable<Feed> GetUserFeeds(Author owner)
        {
            return _context.Feeds
                .AsNoTracking()
                .Where(x => x.Owner == owner);
        }

        public async Task UpdateFeed(Feed feed)
        {
            _context.Feeds.Update(feed);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeed(Feed feed)
        {
            _context.Feeds.Remove(feed);

            await _context.SaveChangesAsync();
        }
    }
}
