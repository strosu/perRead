using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace PerRead.Backend.Models.BackEnd
{
    public class PaymentTransaction
    {
        public string TransactionId { get; set; }

        public Wallet Source { get; set; }

        public Wallet Destination { get; set; }

        public TransactionType TransactionType { get; set; }

        public long TokenAmount { get; set; }
}

    public enum TransactionType {
        [EnumMember(Value = nameof(TokenPurchase))]
        TokenPurchase,

        [EnumMember(Value = nameof(TokenWithdrawal))]
        TokenWithdrawal,

        [EnumMember(Value = nameof(MoveToEscrow))]
        MoveToEscrow,

        [EnumMember(Value = nameof(MoveFromEscrow))]
        MoveFromEscrow
    }
}
