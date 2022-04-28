namespace PerRead.Backend.Models.FrontEnd
{
    public class FEArticle
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public uint Price { get; set; }

        public IEnumerable<FEAuthorPreview> AuthorPreviews { get; set; }

        public IEnumerable<FETagPreview> TagPreviews { get; set; }

        public string ArticleImageUrl { get; set; }
    }
}
