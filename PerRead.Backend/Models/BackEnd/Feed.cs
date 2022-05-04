using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Models.BackEnd
{
    public class Feed
    {
        public string FeedId { get; set; }

        public string FeedName { get; set; }

        public Author Owner { get; set; }

        public IList<SectionFeedMapping> SubscribedSections { get; set; }

        public int RequireConfirmationAbove { get; set; }

        public bool ShowFreeArticles { get; set; }

        public bool ShowArticlesAboveConfirmationLimit { get; set; }

        public bool ShowUnaffordableArticles { get; set; }
    }
}

public class SectionFeedMapping
{
    public string SectionId { get; set; }

    public Section Section { get; set; }

    public string FeedId { get; set; }

    public Feed Feed { get; set; }
}
