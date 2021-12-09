namespace PerRead.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public uint Price { get; set; }
    }
}
