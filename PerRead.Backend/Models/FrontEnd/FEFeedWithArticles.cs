namespace PerRead.Backend.Models.FrontEnd
{
    public class FEFeedPreview
    {
        public string FeedId { get; set; }

        public string FeedName { get; set; }
    }

    public class FEFeedDetails
    {
        public string FeedId { get; set; }

        public string FeedName { get; set; }

        public IEnumerable<FEAuthorPreview> SubscribedAuthors { get; set; }

        public int RequireConfirmationAbove { get; set; } = 1;

        public bool ShowFreeArticles { get; set; } = true;

        public bool ShowArticlesAboveConfirmationLimit { get; set; } = true;

        public bool ShowUnaffordableArticles { get; set; } = true;
    }

    public class FEFeedWithArticles
    {
        public string FeedId { get; set; }

        public string FeedName { get; set; }

        public IEnumerable<FEArticlePreview> ArticlePreviews { get; set; }
    }
}
