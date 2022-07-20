using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
{
            _context = context;
        }

        public async Task<TransactionResult> AddTransaction(Wallet from, Wallet to, long amount, TransactionType type)
        {
            var validation = Validate(from, amount);

            if (validation.Result == PaymentResultEnum.Failed)
            {
                return validation;
            }

            var transaction = new PaymentTransaction
            {
                PaymentTransactionId = Guid.NewGuid().ToString(),
                SourceWalletId = from.WalledId,
                DestinationWalletId = to.WalledId,
                TransactionType = type,
                TokenAmount = amount
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            validation.Transaction = transaction;

            return validation;
        }

        private TransactionResult Validate(Wallet from, long amount)
        {
            if (amount < 0)
            {
                return new TransactionResult
                {
                    Result = PaymentResultEnum.Failed,
                    Reason = "Invalid amount"
                };
            }

            if (from.TokenAmount < amount)
            {
                return new TransactionResult
                {
                    Result = PaymentResultEnum.Failed,
                    Reason = $"Insufficient funds, please use a value no more than {from.TokenAmount}"
                };
            }

            return new TransactionResult
            {
                Result = PaymentResultEnum.Success
            };
        }
    }

    public interface ITransactionRepository 
    {
        Task<TransactionResult> AddTransaction(Wallet from, Wallet to, long amount, TransactionType type);
    }

    public class TransactionResult
    {
        public static TransactionResult Success { get; set; } = new TransactionResult
        {
            Result = PaymentResultEnum.Success
        };

        public PaymentResultEnum Result;

        public string? Reason;

        public PaymentTransaction Transaction { get; set; }
    }

    // TODO - rename this
    public enum PaymentResultEnum
    {
        Success = 0,
        Failed = 1
    }

}
