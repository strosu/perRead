using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class WalletExtensions
    {
        public static FEWallet ToFEWallet(this Wallet wallet)
        {
            return new FEWallet
            {
                WalletId = wallet.WalledId,
                TokenAmount = wallet.TokenAmount,
                IncomingTransactions = wallet.IncomingTransactions.Select(x => x.ToFETransactionPreview()),
                OutgoingTransactions = wallet.OutgoingTransactions.Select(x => x.ToFETransactionPreview())
            };
        }

        public static FEWalletPreview ToFEWalletPreview(this Wallet wallet)
        {
            return new FEWalletPreview
            {
                WalletId = wallet.WalledId,
                TokenAmount = wallet.TokenAmount
            };
        }
    }
}
