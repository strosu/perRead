namespace PerRead.Backend.Models
{
    public class Article
    {
        public int ArticleId { get; set; }

        public Author Author { get; set; }

        public string Title { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public uint Price { get; set; }
    }
}