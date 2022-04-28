using System;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class FeedExtensions
    {
        public static FEFeedPreview ToFEFeedPreview(this Feed feed)
        {
            return new FEFeedPreview
            {
                FeedId = feed.FeedId,
                FeedName = feed.FeedName,
            };
        }

        public static FEFeedWithArticles ToFEFeed(this Feed feed, IEnumerable<FEArticlePreview> articles = null)
        {
            // TODO - mayybe revisit passing null as a default
            return new FEFeedWithArticles
            {
                FeedId = feed.FeedId,
                FeedName = feed.FeedName,
                ArticlePreviews = articles
            };
        }

        public static FEFeedDetails ToFEFeedInfo(this Feed feed)
        {
            return new FEFeedDetails
            {
                FeedId = feed.FeedId,
                FeedName = feed.FeedName,
                SubscribedSections = feed.SubscribedSections.Select(x => x.ToFESectionPreview()),
                RequireConfirmationAbove = feed.RequireConfirmationAbove,
                ShowFreeArticles = feed.ShowFreeArticles,
                ShowArticlesAboveConfirmationLimit = feed.ShowArticlesAboveConfirmationLimit,
                ShowUnaffordableArticles = feed.ShowUnaffordableArticles
            };
        }
    }
}

