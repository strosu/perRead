using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Models.FrontEnd
{
    public class FEWalletPreview
    {
        public string WalletId { get; set; }

        public long TokenAmount { get; set; }
    }

    public class FEWallet
    {
        public string WalletId { get; set; }

        public long TokenAmount { get; set; }

        public IEnumerable<FETransactionPreview> IncomingTransactions { get; set; }

        public IEnumerable<FETransactionPreview> OutgoingTransactions { get; set; }
    }

    public class FETransactionPreview
    {
        public string TransactionId { get; set; }

        public string SourceWalletId { get; set; }

        public string DestinationWalletId { get; set; }

        public TransactionType TransactionType { get; set; }

        public long TokenAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Comment { get; set; }

        public bool IsSender { get; internal set; }
    }

    public class FETransaction
    {
        public string TransactionId { get; set; }

        public string SourceWalletId { get; set; }

        public string DestinationWalletId { get; set; }

        public TransactionType TransactionType { get; set; }

        public long TokenAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public FEAuthorPreview Sender { get; set; }

        public FEAuthorPreview Receiver { get; set; }

        public string Comment { get; set; }

        public bool IsSender { get; internal set; }
    }
}
