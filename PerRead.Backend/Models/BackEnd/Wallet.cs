namespace PerRead.Backend.Models.BackEnd
{
    public class Wallet
    {
        public static Wallet EmptyWallet = new Wallet
        {
            Owner = new Author(),
            TokenAmount = 0,
            WalledId = new Guid().ToString()
        };

        public string WalledId { get; set; }

        public Author? Owner { get; set; }

        public long TokenAmount { get; set; }

        public IList<PaymentTransaction> IncomingTransactions { get; set; }
        
        public IList<PaymentTransaction> OutgoingTransactions { get; set; }
    }
}
