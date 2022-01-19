namespace PerRead.Backend.Models.BackEnd
{
    /// <summary>
    /// Tag, as loaded from the DB
    /// </summary>
    public class TagModel
    {
        public int TagId { get; set; }

        public string TagName { get; set; }

        public IEnumerable<ArticleModel> Articles { get; set; }
    }
}
