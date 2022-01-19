namespace PerRead.Backend.Models.FrontEnd
{
    public class Tag
    {
        public int TagId { get; set; }

        public string TagName { get; set; }

        public IEnumerable<ArticlePreview> ArticlePreviews { get; set; }
    }
}
