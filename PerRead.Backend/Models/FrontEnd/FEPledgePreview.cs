namespace PerRead.Backend.Models.FrontEnd
{
    public class FEPledgePreview
    {
        public string RequestPledgeId { get; set; }

        public FEAuthorPreview Pledger { get; set; }

        public uint TotalTokenSum { get; set; }
    }
}

