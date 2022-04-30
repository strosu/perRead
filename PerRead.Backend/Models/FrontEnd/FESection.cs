namespace PerRead.Backend.Models.FrontEnd
{
    public class FESectionWithArticles
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string SectionId { get; set; }

        public IEnumerable<FEArticlePreview> ArticlePreviews { get; set; }

        // Should hold a map of whether each feed of the current user is subscribed to this section
        // Based on this, construct the UI
        public IEnumerable<FeedSubscriptionToSection> FeedSectionStatus { get; set; }
    }

    public class FESectionPreview
    {
        public string Name { get; set; }

        public string SectionId { get; set; }
    }

    public class FeedSubscriptionToSection
    {
        public FEFeedPreview FeedPreview { get; set; }

        public bool IsSubscribedToSection { get; set; }
    }
}
