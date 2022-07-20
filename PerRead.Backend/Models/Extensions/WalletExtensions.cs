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
