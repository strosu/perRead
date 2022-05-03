using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Repositories
{
    public interface IFeedRepository
    {
        Task<Feed> CreateNewFeed(Author owner, string name);

        Task AddToFeed(string feedId, Section sectionToAdd);

        Task RemoveFromFeed(string feedId, Section sectionToRemove);

        Task<Feed> GetFeedInfo(string feedId);

        Task<Feed> GetFeedWithSections(string feedId);

        IQueryable<Feed> GetUserFeeds(Author owner);

        //IQueryable<Author> GetAuthors(string feedId);

        IQueryable<Section> GetSections(string feedId);

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

        public async Task<Feed> GetFeedWithSections(string feedId)
        {
            return await _context.Feeds
                .Where(x => x.FeedId == feedId)
                .Include(x => x.SubscribedSections).FirstOrDefaultAsync();
        }

        public async Task AddToFeed(string feedId, Section sectionToAdd)
        {
            var feed = await _context.Feeds.Where(x => x.FeedId == feedId).Include(x => x.SubscribedSections).SingleAsync();

            if (feed == null)
            {
                throw new ArgumentException("feed does not exist");
            }

            if (feed.SubscribedSections == null)
            {
                feed.SubscribedSections = new List<Section>();
            }

            feed.SubscribedSections.Add(sectionToAdd);

            await _context.SaveChangesAsync();
        }

        public IQueryable<Section> GetSections(string feedId)
        {
            return _context.Feeds
                .AsNoTracking().Where(x => x.FeedId == feedId)
                .Include(x => x.SubscribedSections)
                .SelectMany(x => x.SubscribedSections);
        }


        //public IQueryable<Author> GetAuthors(string feedId)
        //{
        //    return _context.Feeds
        //        .AsNoTracking().Where(x => x.FeedId == feedId)
        //        .Include(x => x.SubscribedAuthors)
        //        .ThenInclude(x => x.PublishSections)
        //        .ThenInclude(x => x.Articles)
        //        .ThenInclude(x => x.Article)
        //        .ThenInclude(x => x.ArticleAuthors)
        //        .ThenInclude(x => x.Author)
        //        .SelectMany(f => f.SubscribedAuthors);
        //}

        public IQueryable<Feed> GetUserFeeds(Author owner)
        {
            return _context.Feeds
                .AsNoTracking()
                .Where(x => x.Owner == owner)
                .Include(x => x.SubscribedSections);
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

        public async Task RemoveFromFeed(string feedId, Section sectionToRemove)
        {
            var feed = await _context.Feeds.Where(x => x.FeedId == feedId)
                .Include(x => x.SubscribedSections).SingleAsync();

            if (feed == null)
            {
                throw new ArgumentException("feed does not exist");
            }

            feed.SubscribedSections.Remove(sectionToRemove);

            await _context.SaveChangesAsync();
        }
    }
}
