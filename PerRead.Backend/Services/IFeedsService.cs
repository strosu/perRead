using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Constants;
using PerRead.Backend.Helpers.Errors;
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

        Task AddSectionToFeed(string feedId, string sectionId);

        Task UpdateFeedInfo(string feedId, FEFeedDetails feedDetails);

        Task DeleteFeed(string feedId);

        Task AddSectionToFeeds(string sectionId, IEnumerable<string> subscribedFeedIds);
    }

    public class FeedsService : IFeedsService
    {
        private readonly IFeedRepository _feedsRepository;
        private readonly IRequesterGetter _requesterGetter;
        private readonly ISectionRepository _sectionRepository;

        public FeedsService(IFeedRepository feedsRepository, IAuthorRepository authorRepository, IRequesterGetter requesterGetter, ISectionRepository sectionRepository)
        {
            _feedsRepository = feedsRepository;
            _requesterGetter = requesterGetter;
            _sectionRepository = sectionRepository;
        }

        public async Task<IEnumerable<FEFeedPreview>> GetFeeds()
        {
            var owner = await _requesterGetter.GetRequester();
            var feeds = await _feedsRepository.GetUserFeeds(owner).ToListAsync();

            if (!feeds.Any())
            {
                var defaultFeed = await _feedsRepository.CreateNewFeed(owner, BusinessConstants.DefaultFeedName);
                return new List<FEFeedPreview> {
                    defaultFeed.ToFEFeedPreview()
                };

            }

            return feeds.Select(x => x.ToFEFeedPreview());
        }

        public async Task<FEFeedWithArticles> CreateNewFeed(string feedName)
        {
            var owner = await _requesterGetter.GetRequester();

            if (owner == null)
            {
                throw new NotFoundException("Could not find the user for which to create the feed.");
            }

            var feed = await _feedsRepository.CreateNewFeed(owner, feedName);

            return feed.ToFEFeed();
        }

        public async Task AddSectionToFeed(string feedId, string sectionId)
        {
            var feed = await _feedsRepository.GetFeedInfo(feedId);

            var requester = await _requesterGetter.GetRequester();

            if (feed.Owner.AuthorId != requester.AuthorId)
            {
                throw new UnauthorizedException("Cannot add for someone else lol.");
            }

            var section = await _sectionRepository.GetSection(sectionId);

            if (section == null)
            {
                throw new NotFoundException("Could not find the section to subscribe to.");
            }

            await _feedsRepository.AddToFeed(feedId, section);
        }

        public async Task<FEFeedWithArticles> GetFeedArticles(string feedId)
        {
            var feed = await _feedsRepository.GetFeedInfo(feedId);

            var requester = await _requesterGetter.GetRequesterWithArticles();

            var articleQuery = _feedsRepository.GetFeedQuery(feedId)
                .Select(x => x.SubscribedSections).SelectMany(x => x)
                .SelectMany(x => x.Section.Articles)
                .Include(x => x.Article.Sections)
                .ThenInclude(x => x.Section)
                .Include(x => x.Article.Tags)
                .Include(x => x.Article.AuthorsLink)
                .ThenInclude(x => x.Author)
                .Select(x => x.Article);

            var filteredQuery = ApplyFeedFilters(articleQuery, feed, requester)
                .Distinct()
                .OrderByDescending(x => x.CreatedAt).Take(20)
                .Select(x => x.ToFEArticlePreview(requester));

            var articles = await filteredQuery.ToListAsync();

            return feed.ToFEFeed(articles);
        }

        public async Task<FEFeedDetails> GetFeedInfo(string feedId)
        {
            var feed = await _feedsRepository.GetFeedWithSections(feedId);

            if (feed == null)
            {
                throw new NotFoundException($"Could not find feed with Id {feedId}");
            }

            return feed.ToFEFeedInfo();
        }

        public async Task UpdateFeedInfo(string feedId, FEFeedDetails feedDetails)
        {
            var feed = await _feedsRepository.GetFeedWithSections(feedId);
            UpdateFeed(feed, feedDetails);

            await _feedsRepository.UpdateFeed(feed);
        }

        public async Task DeleteFeed(string feedId)
        {
            var feed = await _feedsRepository.GetFeedInfo(feedId);
            var requester = await _requesterGetter.GetRequester();

            // Only the owner can delete a feed
            if (feed.Owner.AuthorId != requester.AuthorId)
            {
                throw new UnauthorizedException("You don't own this feed");
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
                articles = articles.Where(x => x.Price <= requester.MainWallet.TokenAmount);
            }

            return articles;
        }

        private static void UpdateFeed(Feed feed, FEFeedDetails feedDetails)
        {
            feed.FeedName = feedDetails.FeedName;

            var subscribedSectionIds = feedDetails.SubscribedSections.Select(x => x.SectionId);
            var unsubscribedAuthors = feed.SubscribedSections.Where(x => !subscribedSectionIds.Contains(x.SectionId));

            foreach (var unsub in unsubscribedAuthors)
            {
                feed.SubscribedSections.Remove(unsub);
            }

            feed.RequireConfirmationAbove = feedDetails.RequireConfirmationAbove;
            feed.ShowFreeArticles = feedDetails.ShowFreeArticles;
            feed.ShowArticlesAboveConfirmationLimit = feedDetails.ShowArticlesAboveConfirmationLimit;
            feed.ShowUnaffordableArticles = feedDetails.ShowUnaffordableArticles;
        }

        public async Task AddSectionToFeeds(string sectionId, IEnumerable<string> subscribedFeedIds)
        {
            var requester = await _requesterGetter.GetRequester();

            var userFeeds = await _feedsRepository.GetUserFeeds(requester).ToListAsync();

            if (subscribedFeedIds.Any(x => !userFeeds.Any(feed => feed.FeedId == x)))
            {
                throw new UnauthorizedException("One or more feed don't belong to the current user");
            }

            var section = await _sectionRepository.GetSection(sectionId);

            if (section == null)
            {
                throw new NotFoundException("Could not find the section to subscribe to");
            }

            (var toSubscribe, var toUnsub) = GetList(userFeeds, subscribedFeedIds, section);

            var addSectionTasks = toSubscribe.Select(x => _feedsRepository.AddToFeed(x, section));
            var removeSectionTasks = toUnsub.Select(x => _feedsRepository.RemoveFromFeed(x, section));

            var sectionTasks = addSectionTasks.Concat(removeSectionTasks);

            await Task.WhenAll(sectionTasks);
        }

        private (IEnumerable<string> toSubscribeTo, IEnumerable<string> toUnsubscribeFrom) GetList(IEnumerable<Feed> allFeeds, IEnumerable<string> targetFeedIds, Section section)
        {
            var toSubscribeTo = new List<string>();
            var toUnsubscribeFrom = new List<string>();

            foreach (var feed in allFeeds)
            {
                if (feed.SubscribedSections.Any(x => x.SectionId == section.SectionId))
                {
                    if (targetFeedIds.Contains(feed.FeedId))
                    {
                        // Feed should contain the subscription, and it does
                        continue;
                    }

                    toUnsubscribeFrom.Add(feed.FeedId);
                }

                if (!targetFeedIds.Contains(feed.FeedId))
                {
                    // Feed should not contain it, and it doesn't
                    continue;
                }

                toSubscribeTo.Add(feed.FeedId);
            }

            return (toSubscribeTo, toUnsubscribeFrom);
        }
    }
}
