namespace PerRead.Backend.Models.FrontEnd
{
    public class FETag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<FEArticlePreview> ArticlePreviews { get; set; }

        public DateTime FirstUsage { get; set; }
    }
}
