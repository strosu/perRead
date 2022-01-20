namespace PerRead.Backend.Models.FrontEnd
{
    public class FEAuthor 
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<FEArticlePreview> ArticlePreviews { get; set; }
    }
}
