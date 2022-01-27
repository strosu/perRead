namespace PerRead.Backend.Models.BackEnd
{
    /// <summary>
    /// Article as loaded from the Database
    /// </summary>
    public class Article
    {
        public int ArticleId { get; set; }

        public ICollection<ArticleAuthor> ArticleAuthors { get; set; }

        public string Title { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public uint Price { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ImageUrl { get; set; }
    }

    /// <summary>
    /// Connection between Article and Authors, as loaded from the DB
    /// </summary>
    public class ArticleAuthor
    {
        public Article Article { get; set; }

        public int ArticleId { get; set; }

        public Author Author { get; set; }

        public string AuthorId { get; set; }

        public int Order { get; set; }
    }
}