namespace PerRead.Backend.Models
{
    public class Article
    {
        public int ArticleId { get; set; }

        public int AuthorId { get; set; }

        public string Title { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public uint Price { get; set; }
    }
}