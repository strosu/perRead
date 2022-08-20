namespace PerRead.Backend.Models.BackEnd
{
    public class ArticleUnlock
    {
        public string Id { get; set; }

        public string AuthorId { get; set; }

        public Author Author { get; set; }

        public string ArticleId { get; set; }

        public Article Article { get; set; }

        public DateTime AquisitionDate { get; set; }

        public uint AquisitionPrice { get; set; }

        //public PaymentTransaction? CorrespondingTransaction { get; set; }

        //public ArticleReview? Review { get; set; }

        public bool? Recommends { get; set; }
    }
}
