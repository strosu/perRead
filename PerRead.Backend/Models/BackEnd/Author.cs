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

        /// <summary>
        /// DO NOT USE THIS. Here just for EF convenience
        /// TODO - configure it differently to get rid of this
        /// </summary>
        public IList<ArticleUnlock> UnlockedArticles { get; set; }

        /// <summary>
        /// DO NOT USE THIS. Here just for EF convenience
        /// TODO - configure it differently to get rid of this
        /// </summary>
        public IList<Feed> SubscribedFeeds { get; set; }

        /// <summary>
        /// The price under which articles do not require confirmation
        /// </summary>
        public uint RequireConfirmationAbove { get; set; } = 10;
    }
}
