namespace PerRead.Backend.Models.BackEnd
{
    public class Section
    {
        public string SectionId { get; set; }

        public string Name { get; set; }

        public string Description;

        public string AuthorId { get; set; }

        public Author Author { get; set; }

        public IEnumerable<SectionArticle> Articles { get; set; }

        /// <summary>
        /// DO NOT USE THIS. Here just for EF convenience
        /// TODO - configure it differently to get rid of this
        /// </summary>
        //public IEnumerable<Feed> SubscribedFeeds { get; set; }
    }

    public class SectionArticle
    {
        public string SectionId { get; set; }

        public Section Section { get; set; }

        public Article Article { get; set; }

        public string ArticleId { get; set; }
    }
}
