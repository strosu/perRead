using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Repositories
{
    public interface IFeedRepository
    {
        Task<Feed> CreateNewFeed(Author owner, string name);

        Task AddToFeed(string feedId, Author authorToAdd);

        IQueryable<Author> GetAuthors(string feedId);
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
            };

            _context.Feeds.Add(feed);

            await _context.SaveChangesAsync();

            return feed;
        }

        public async Task AddToFeed(string feedId, Author authorToAdd)
        {
            var feed = await _context.Feeds.FindAsync(feedId);
            
            if (feed == null)
            {
                throw new ArgumentException("feed does not exist");
            }

            feed.SubscribedAuthors.Add(authorToAdd);

            await _context.SaveChangesAsync();
        }

        public IQueryable<Author> GetAuthors(string feedId)
        {
            return _context.Feeds
                .AsNoTracking().Where(x => x.FeedId == feedId)
                .Include(x => x.SubscribedAuthors)
                .ThenInclude(x => x.Articles)
                .ThenInclude(x => x.Article)
                .SelectMany(f => f.SubscribedAuthors);
        }
    }
}
