using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace PerRead.Backend.Models.BackEnd
{
    public class PaymentTransaction
    {
        public string PaymentTransactionId { get; set; }

        public string SourceWalletId { get; set; }

        public Wallet SourceWallet { get; set; }

        public string DestinationWalletId { get; set; }

        public Wallet DestinationWallet { get; set; }

        public TransactionType TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        public long TokenAmount { get; set; }

        public string? Comment { get; set; }

        public string TransactionTrackingId { get; set; }
    }

    public enum TransactionType
    {
        [EnumMember(Value = nameof(TokenPurchase))]
        TokenPurchase,

        [EnumMember(Value = nameof(TokenWithdrawal))]
        TokenWithdrawal,

        [EnumMember(Value = nameof(ArticleRead))]
        ArticleRead,

        [EnumMember(Value = nameof(PledgeCreated))]
        PledgeCreated,

        [EnumMember(Value = nameof(PledgeCancelled))]
        PledgeCancelled,

        [EnumMember(Value = nameof(PledgeEdited))]
        PledgeEdited,

        [EnumMember(Value = nameof(RequestAccepted))]
        RequestAccepted,

        [EnumMember(Value = nameof(RequestCompleted))]
        RequestCompleted,

        [EnumMember(Value = nameof(RequestCancelled))]
        RequestCancelled,

        [EnumMember(Value = nameof(RequestRefused))]
        RequestRefused
    }
}
