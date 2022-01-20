namespace PerRead.Backend.Models.BackEnd
{
    /// <summary>
    /// Tag, as loaded from the DB
    /// </summary>
    public class Tag
    {
        public int TagId { get; set; }

        public string TagName { get; set; }

        public IEnumerable<Article> Articles { get; set; }

        public DateTime FirstUsage { get; set; }
    }
}
