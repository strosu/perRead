namespace PerRead.Backend.Models.FrontEnd
{
    /// <summary>
    /// Minimialist information, for displaying the tabs
    /// </summary>
    public class FEFeedPreview
    {
        public string FeedId { get; set; }

        public string FeedName { get; set; }
    }

    /// <summary>
    /// Feed options
    /// </summary>
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

    /// <summary>
    /// Entire feed with articles
    /// </summary>
    public class FEFeedWithArticles
    {
        public string FeedId { get; set; }

        public string FeedName { get; set; }

        public IEnumerable<FEArticlePreview> ArticlePreviews { get; set; }
    }
}
