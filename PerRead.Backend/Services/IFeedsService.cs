using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Extensions;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IFeedsService
    {
        Task<FEFeed> CreateNewFeed(string feedName);

        Task<FEFeed> GetFeed(string feedId);

        Task<FEFeedInfo> GetFeedInfo(string feedId);

        Task AddAuthorToFeed(string feedId, string authorId);

        Task<IEnumerable<FEFeedPreview>> GetFeeds();
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

        public async Task<IEnumerable<FEFeedPreview>> GetFeeds()
        {
            var currentUser = _accessor.GetUserId();
            var owner = await _authorRepository.GetAuthor(currentUser).FirstOrDefaultAsync();

            var feeds = await _feedsRepository.GetUserFeeds(owner).ToListAsync();

            if (!feeds.Any())
            {
                var defaultFeed = await _feedsRepository.CreateNewFeed(owner, "defaultFEEEDNAME");
                return new List<FEFeedPreview> {
                    defaultFeed.ToFEFeedPreview()
                };

            }

            return feeds.Select(x => x.ToFEFeedPreview());
        }

        public async Task<FEFeed> CreateNewFeed(string feedName)
        {
            var currentUser = _accessor.GetUserId();
            var owner = await _authorRepository.GetAuthor(currentUser).FirstOrDefaultAsync();

            if (owner == null)
            {
                throw new ArgumentException("could not find the user for which to create the feed");
            }

            var feed = await _feedsRepository.CreateNewFeed(owner, feedName);

            return feed.ToFEFeed();
        }

        public async Task AddAuthorToFeed(string feedId, string authorId)
        {
            var feed = await _feedsRepository.GetFeedInfo(feedId);

            var requester = _accessor.GetUserId();

            if (feed.Owner.AuthorId != requester)
            {
                throw new ArgumentException("cannot add for someone else lol");
            }

            var author = await _authorRepository.GetAuthor(authorId).FirstOrDefaultAsync();

            if (author == null)
            {
                throw new ArgumentException("could not find the author to subscribe to");
            }

            await _feedsRepository.AddToFeed(feedId, author);
        }

        public async Task<FEFeed> GetFeed(string feedId)
        {
            var feed = await _feedsRepository.GetFeedInfo(feedId);
            var feedAuthors = _feedsRepository.GetAuthors(feedId);
            var articleAuthors = feedAuthors.Select(x => x.Articles).SelectMany(x => x);

            var requester = await _authorRepository.GetAuthorWithReadArticles(_accessor.GetUserId()).SingleAsync();

            var articlesQuery =
                articleAuthors.Select(x => x.Article)
                .OrderByDescending(x => x.CreatedAt).Take(20)
                .Select(x => x.ToFEArticlePreview(requester));

            var articles = await articlesQuery.ToListAsync();

            return feed.ToFEFeed(articles);
        }

        public async Task<FEFeedInfo> GetFeedInfo(string feedId)
        {
            var feed = await _feedsRepository.GetFeedWithAuthors(feedId);
            return feed.ToFEFeedInfo();
        }
    }
}
