using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Models.Commands
{
    public class CreateRequestCommand
    {
        public string TargetAuthorId { get; set; }

        public RequestCommand RequestCommand { get; set; }

        public PledgeCommand PledgeCommand { get; set; }
    }

    public class PledgeCommand
    {
        public string? PledgeId { get; set; }

        public string? RequestId { get; set; }

        public uint TotalPledgeAmount { get; set; }

        public uint UpfrontPledgeAmount { get; set; }
    }

    public class RequestCommand
    {
        public string? RequestId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public RequestPostPublishState PostPublishState { get; set; }

        public uint PercentForPledgers { get; set; }

        public DateTime Deadline { get; set; }
    }

    public class CompleteRequestCommand
    {
        public string RequestId { get; set; }

        public string ResultingArticleId { get; set; }
    }

    public class AbandonRequestCommand
    {
        public string RequestId { get; set; }

        public string Reason { get; set; }
    }
}

