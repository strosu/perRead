namespace PerRead.Backend.Models
{
    public class Article
    {
        public int ArticleId { get; set; }

        public ICollection<ArticleAuthor> ArticleAuthors { get; set; }

        public string Title { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public uint Price { get; set; }

        public string Content { get; set; }
    }

    public class ArticleAuthor
    {
        public Article Article { get; set; }

        public int ArticleId { get; set; }

        public Author Author { get; set; }

        public string AuthorId { get; set; }

        public int Order { get; set; }
    }
}