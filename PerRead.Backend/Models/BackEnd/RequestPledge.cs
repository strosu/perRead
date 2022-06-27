namespace PerRead.Backend.Models.BackEnd
{
    public class RequestPledge
    {
        public string RequestPledgeId { get; set; }

        public ArticleRequest ParentRequest { get; set; }

        public Author Pledger { get; set; }

        public uint TotalTokenSum { get; set; }

        public uint TokensOnAccept { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

