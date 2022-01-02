namespace PerRead.Backend.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        public string Name { get; set; }

        public int PopularityRank { get; set; }

        public IEnumerable<ArticleAuthor> Articles { get; set; }
    }
}
