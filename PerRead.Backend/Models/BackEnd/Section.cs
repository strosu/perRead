namespace PerRead.Backend.Models.BackEnd
{
    public class Section
    {
        public string SectionId { get; set; }

        public string Name { get; set; }

        public string AuthorId { get; set; }

        public Author Author { get; set; }

        public IEnumerable<SectionArticle> Articles { get; set; }
    }

    public class SectionArticle
    {
        public string SectionId { get; set; }

        public Section Section { get; set; }

        public Article Article { get; set; }

        public string ArticleId { get; set; }
    }
}
