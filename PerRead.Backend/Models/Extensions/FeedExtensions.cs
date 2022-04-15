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

        public static FEFeed ToFEFeed(this Feed feed, IEnumerable<FEArticlePreview> articles = null)
        {
            // TODO - mayybe revisit passing null as a default
            return new FEFeed
            {
                FeedId = feed.FeedId,
                FeedName = feed.FeedName,
                ArticlePreviews = articles
            };
        }

        public static FEFeedInfo ToFEFeedInfo(this Feed feed)
        {
            return new FEFeedInfo
            {
                FeedId = feed.FeedId,
                FeedName = feed.FeedName,
                SubscribedAuthors = feed.SubscribedAuthors.Select(x => x.ToFEAuthorPreview())
            };
        }
    }
}

