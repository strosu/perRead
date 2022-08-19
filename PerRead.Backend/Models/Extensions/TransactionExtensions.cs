using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class TransactionExtensions
    {
        public static FETransaction ToFETransaction(this PaymentTransaction transaction, bool isSender)
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
                Receiver = transaction.DestinationWallet.Owner.ToFEAuthorPreview(),
                Comment = transaction.Comment,
                IsSender = isSender
            };
        }

        public static FETransactionPreview ToFETransactionPreview(this PaymentTransaction transaction, bool isSender)
        {
            return new FETransactionPreview
            {
                TransactionId = transaction.PaymentTransactionId,
                DestinationWalletId = transaction.DestinationWalletId,
                SourceWalletId = transaction.SourceWalletId,
                TokenAmount = transaction.TokenAmount,
                TransactionType = transaction.TransactionType,
                TransactionDate = transaction.TransactionDate,
                Comment = transaction.Comment,
                IsSender = isSender
            };
        }
    }
}
