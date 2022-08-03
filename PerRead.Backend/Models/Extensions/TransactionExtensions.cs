using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class TransactionExtensions
    {
        public static FETransaction TOFETransaction(this PaymentTransaction transaction)
        {
            return new FETransaction()
            {
                TransactionId = transaction.PaymentTransactionId,
                SourceWalletId = transaction.SourceWalletId,
                DestinationWalletId = transaction.DestinationWalletId,
                TokenAmount = transaction.TokenAmount,
                TransactionType = transaction.TransactionType,
                TransactionDate = transaction.TransactionDate,
                Sender = transaction.SourceWallet.Owner.ToFEAuthorPreview(),
                Receiver = transaction.DestinationWallet.Owner.ToFEAuthorPreview()
            };
        }

        public static FETransactionPreview ToFETransactionPreview(this PaymentTransaction transaction)
        {
            return new FETransactionPreview
            {
                TransactionId = transaction.PaymentTransactionId,
                DestinationWalletId = transaction.DestinationWalletId,
                SourceWalletId = transaction.SourceWalletId,
                TokenAmount = transaction.TokenAmount,
                TransactionType = transaction.TransactionType,
                TransactionDate = transaction.TransactionDate
            };
        }
    }
}
