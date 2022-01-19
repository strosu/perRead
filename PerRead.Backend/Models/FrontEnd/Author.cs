namespace PerRead.Backend.Models.FrontEnd
{
    public class Author 
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ArticlePreview> ArticlePreviews { get; set; }
    }
}
