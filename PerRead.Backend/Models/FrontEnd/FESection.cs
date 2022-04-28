namespace PerRead.Backend.Models.FrontEnd
{
    public class FESectionWithArticles
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string SectionId { get; set; }

        public IEnumerable<FEArticlePreview> ArticlePreviews { get; set; }
    }

    public class FESectionPreview
    {
        public string Name { get; set; }

        public string SectionId { get; set; }
    }
}
