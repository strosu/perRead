namespace PerRead.Backend.Models
{
    public class Author
    {
        public string AuthorId { get; set; }

        public string Name { get; set; }

        public int PopularityRank { get; set; }

        public IEnumerable<Article> Articles { get; set; }
    }
}
