namespace PerRead.Backend.Models.BackEnd
{
    public class Feed
    {
        public string FeedId { get; set; }

        public string FeedName { get; set; }

        public Author Owner { get; set; }

        public IList<Author> SubscribedAuthors { get; set; }

        public int RequireConfirmationAbove { get; set; }

        public bool ShowFreeArticles { get; set; }

        public bool ShowArticlesAboveConfirmationLimit { get; set; }

        public bool ShowUnaffordableArticles { get; set; }
    }
}
