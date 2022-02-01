namespace PerRead.Backend.Models.BackEnd
{
    /// <summary>
    /// Authors, as loaded from the DB
    /// </summary>
    public class Author
    {
        /// <summary>
        /// This is the same as the userId from the Users table
        /// </summary>
        public string AuthorId { get; set; }

        public string Name { get; set; }

        public int PopularityRank { get; set; }

        public IEnumerable<ArticleAuthor> Articles { get; set; }

        public string? ProfileImageUri { get; set; }

        public long ReadingTokens { get; set; }
    }
}
