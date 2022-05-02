using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class SectionExtensions
    {
        public static FESectionWithArticles ToFESectionWithArticles(this Section section, Author requester, IEnumerable<Feed> feeds)
        {
            return new FESectionWithArticles
            {
                Name = section.Name,
                Description = section.Description,
                SectionId = section.SectionId,
                ArticlePreviews = section.Articles?.OrderByDescending(a => a.Article.CreatedAt).Select(x => x.Article.ToFEArticlePreview(requester)),
                FeedSubscriptionStatuses = feeds.Select(x => x.ToSectionSubscription(section))
            };
        }

        public static FESectionPreview ToFESectionPreview(this Section section)
        {
            return new FESectionPreview
            {
                Name = section.Name,
                SectionId = section.SectionId
            };
        }

        public static SectionSubscriptonStatus ToSectionSubscription(this Feed feed, Section section)
        {
            return new SectionSubscriptonStatus
            {
                Feed = feed.ToFEFeedPreview(),
                IsSubscribedToSection = feed.SubscribedSections.Any(x => x.SectionId == section.SectionId)
            };
        }
    }
}

