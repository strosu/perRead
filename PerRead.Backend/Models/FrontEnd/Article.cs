namespace PerRead.Backend.Models.FrontEnd
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public IEnumerable<AuthorPreview> AuthorPreviews { get; set; }

        public IEnumerable<TagPreview> TagPreviews { get; set; }
    }
}
