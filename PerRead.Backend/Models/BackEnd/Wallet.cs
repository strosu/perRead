namespace PerRead.Backend.Models.BackEnd
{
    public class Wallet
    {
        public static Wallet EmptyWallet = new Wallet
        {
            OwnerId = new Guid().ToString(),
            TokenAmount = 0,
            WalledId = new Guid().ToString()
        };

        public string WalledId { get; set; }

        public string OwnerId { get; set; }

        public long TokenAmount { get; set; }

        public IList<PaymentTransaction> Transactions { get; set; }
    }
}
