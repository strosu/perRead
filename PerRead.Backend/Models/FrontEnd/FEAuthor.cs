namespace PerRead.Backend.Models.FrontEnd
{
    public class FEAuthor
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<FESectionPreview> SectionPreviews { get; set; }

        public string AuthorImageUri { get; set; }

        public int ArticleCount { get; set; }
    }
}
