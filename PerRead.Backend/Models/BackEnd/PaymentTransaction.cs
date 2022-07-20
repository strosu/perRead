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
