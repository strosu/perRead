namespace PerRead.Backend.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        public string TagName { get; set; }

        public IEnumerable<ArticleTag> Articles { get; set; }
    }

    /// <summary>
    /// Used to configure the many-to-many relation between Articles and Tags
    /// </summary>
    public class ArticleTag
    {
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
