using System.Runtime.Serialization;

namespace PerRead.Backend.Models.BackEnd
{
    public class ArticleRequest
    {
        public string ArticleRequestId { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public Author TargetAuthor { get; set; }

        public Author Initiator { get; set; }

        public IList<RequestPledge> Pledges { get; set; }

        public RequestState RequestState { get; set; }

        public RequestPostPublishState PostPublishState { get; set; }

        public uint PercentForledgers { get; set; }

        public Article? ResultingArticle { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Comments { get; set; }
    }

    public enum RequestPostPublishState
    {
        [EnumMember(Value = nameof(Public))]
        Public,

        [EnumMember(Value = nameof(ProfitShare))]
        ProfitShare,

        [EnumMember(Value = nameof(Exclusive))]
        Exclusive,
    }

    public enum RequestState
    {
        [EnumMember(Value = nameof(Created))]
        Created,

        [EnumMember(Value = nameof(Accepted))]
        Accepted,

        [EnumMember(Value = nameof(Completed))]
        Completed,

        [EnumMember(Value = nameof(Rejected))]
        Rejected,

        [EnumMember(Value = nameof(Expired))]
        Expired,

        [EnumMember(Value = nameof(Cancelled))]
        Cancelled,
    }
}

