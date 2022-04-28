namespace PerRead.Backend.Models.FrontEnd
{
    public class FESection
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<FEArticlePreview> ArticlePreviews { get; set; }
    }
}
