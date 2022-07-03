namespace PerRead.Backend.Models.FrontEnd
{
    public class FEPledge
    {
        public string RequestPledgeId { get; set; }

        public FERequestPreview ParentRequest { get; set; }

        public FEAuthorPreview Pledger { get; set; }

        public uint TotalTokenSum { get; set; }

        public uint TokensOnAccept { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

