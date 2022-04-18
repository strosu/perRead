namespace PerRead.Backend.Models.BackEnd
{
    public class ArticleUnlock
    {
        public long Id { get; set; }
        
        public string AuthorId { get; set; }

        public Author Author { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public DateTime AquisitionDate { get; set; }

        public uint AquisitionPrice { get; set; }
    }
}
