using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Extensions;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IFeedsService
    {
        Task<IEnumerable<FEFeedPreview>> GetFeeds();

        Task<FEFeedWithArticles> GetFeedArticles(string feedId);

        Task<FEFeedWithArticles> CreateNewFeed(string feedName);

        Task<FEFeedDetails> GetFeedInfo(string feedId);

        Task AddAuthorToFeed(string feedId, string authorId);

        Task UpdateFeedInfo(string feedId, FEFeedDetails feedDetails);

        Task DeleteFeed(string feedId);
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

        public async Task<FEFeedWithArticles> CreateNewFeed(string feedName)
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

        public async Task<FEFeedWithArticles> GetFeedArticles(string feedId)
        {
            var feed = await _feedsRepository.GetFeedInfo(feedId);
            var feedAuthors = _feedsRepository.GetAuthors(feedId);
            var articleAuthors = feedAuthors.Select(x => x.Articles).SelectMany(x => x);

            var requester = await _authorRepository.GetAuthorWithReadArticles(_accessor.GetUserId()).SingleAsync();

            var articlesQuery =
                articleAuthors.Select(x => x.Article);

            var filteredQuery = ApplyFeedFilters(articlesQuery, feed, requester)
                .OrderByDescending(x => x.CreatedAt).Take(20)
                .Select(x => x.ToFEArticlePreview(requester));

            var articles = await filteredQuery.ToListAsync();

            return feed.ToFEFeed(articles);
        }

        public async Task<FEFeedDetails> GetFeedInfo(string feedId)
        {
            var feed = await _feedsRepository.GetFeedWithAuthors(feedId);

            if (feed == null)
            {
                throw new ArgumentException($"Could not find feed with Id {feedId}");
            }

            return feed.ToFEFeedInfo();
        }

        public async Task UpdateFeedInfo(string feedId, FEFeedDetails feedDetails)
        {
            var feed = await _feedsRepository.GetFeedWithAuthors(feedId);
            UpdateFeed(feed, feedDetails);

            await _feedsRepository.UpdateFeed(feed);
        }

        public async Task DeleteFeed(string feedId)
        {
            var feed = await _feedsRepository.GetFeedInfo(feedId);
            var requesterId = _accessor.GetUserId();

            // Only the owner can delete a feed
            if (feed.Owner.AuthorId != requesterId)
            {
                throw new ArgumentException("You don't own this feed");
            }

            await _feedsRepository.DeleteFeed(feed);
        }
        
        private IQueryable<Article> ApplyFeedFilters(IQueryable<Article> articles, Feed feed, Author requester)
        {
            if (!feed.ShowFreeArticles)
            {
                articles = articles.Where(x => x.Price > 0);
            }

            if (!feed.ShowArticlesAboveConfirmationLimit)
            {
                articles = articles.Where(x => x.Price <= requester.RequireConfirmationAbove);
            }
            
            if (!feed.ShowUnaffordableArticles)
            {
                articles = articles.Where(x => x.Price <= requester.ReadingTokens);
            }

            return articles;
        }
        
        private static void UpdateFeed(Feed feed, FEFeedDetails feedDetails)
        {
            feed.FeedName = feedDetails.FeedName;

            var subscribedAuthorIds = feedDetails.SubscribedAuthors.Select(x => x.AuthorId);
            var unsubscribedAuthors = feed.SubscribedAuthors.Where(x => !subscribedAuthorIds.Contains(x.AuthorId));

            foreach (var unsub in unsubscribedAuthors)
            {
                feed.SubscribedAuthors.Remove(unsub);
            }

            feed.RequireConfirmationAbove = feedDetails.RequireConfirmationAbove;
            feed.ShowFreeArticles = feedDetails.ShowFreeArticles;
            feed.ShowArticlesAboveConfirmationLimit = feedDetails.ShowArticlesAboveConfirmationLimit;
            feed.ShowUnaffordableArticles = feedDetails.ShowUnaffordableArticles;
        }


    }
}
