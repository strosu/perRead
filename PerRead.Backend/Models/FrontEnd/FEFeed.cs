namespace PerRead.Backend.Models.FrontEnd
{
    public class FEFeed
    {
        public string FeedId { get; set; }

        public string FeedName { get; set; }

        public IEnumerable<FEArticlePreview> ArticlePreviews { get; set; }
    }

    public class FEFeedPreview
    {
        public string FeedId { get; set; }

        public string FeedName { get; set; }

    }
}
