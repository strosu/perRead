namespace PerRead.Backend.Models.BackEnd
{
    /// <summary>
    /// Authors, as loaded from the DB
    /// </summary>
    public class Author
    {
        public string AuthorId { get; set; }

        public string Name { get; set; }

        public int PopularityRank { get; set; }

        public IEnumerable<ArticleAuthor> Articles { get; set; }
    }
}
