namespace PerRead.Backend.Models.BackEnd
{
    /// <summary>
    /// Authors, as loaded from the DB
    /// </summary>
    public class Author
    {
        public static Author NonLoggedInAuthor = new Author
        {
            AuthorId = null,
            PublishSections = Enumerable.Empty<Section>(),
            Name = "Annonymous",
            //MainWallet = Wallet.EmptyWallet,
            //EscrowWallet = Wallet.EmptyWallet,
            RequireConfirmationAbove = 0,
            UnlockedArticles = new List<ArticleUnlock>()
        };

        /// <summary>
        /// This is the same as the userId from the Users table
        /// </summary>
        public string AuthorId { get; set; }

        public string Name { get; set; }

        public int PopularityRank { get; set; }

        public string? ProfileImageUri { get; set; }

        public IEnumerable<Section> PublishSections { get; set; }

        //public string MainWalletId { get; set; }

        //public Wallet MainWallet { get; set; }

        //public string EscrowWalletId { get; set; }

        //public Wallet EscrowWallet { get; set; }

        //public long ReadingTokens { get; set; }

        //public long EscrowTokens { get; set; }

        /// <summary>
        /// DO NOT USE THIS. Here just for EF convenience
        /// TODO - configure it differently to get rid of this
        /// </summary>
        public IList<ArticleUnlock> UnlockedArticles { get; set; }

        /// <summary>
        /// The price under which articles do not require confirmation
        /// </summary>
        public uint RequireConfirmationAbove { get; set; } = 10;

        public int PublishedArticleCount { get; set; }

        public string About { get; set; }

        public IEnumerable<RequestPledge> Pledges { get; set; }

        public IEnumerable<ArticleRequest> Requests { get; set; }
    }
}
