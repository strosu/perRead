namespace PerRead.Backend.Models.BackEnd
{
    /// <summary>
    /// Article as loaded from the Database
    /// </summary>
    public class ArticleModel
    {
        public int ArticleId { get; set; }

        public ICollection<ArticleAuthor> ArticleAuthors { get; set; }

        public string Title { get; set; }

        public ICollection<TagModel> Tags { get; set; }

        public uint Price { get; set; }

        public string Content { get; set; }
    }

    /// <summary>
    /// Connection between Article and Authors, as loaded from the DB
    /// </summary>
    public class ArticleAuthor
    {
        public ArticleModel Article { get; set; }

        public int ArticleId { get; set; }

        public AuthorModel Author { get; set; }

        public string AuthorId { get; set; }

        public int Order { get; set; }
    }
}