using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly AppDbContext _context;

        public WalletRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTransaction(Wallet wallet, PaymentTransaction transaction)
        {
            // If transactioning to, add the amount; otherwise deduct it
            var amount = transaction.Destination == wallet ? transaction.TokenAmount : transaction.TokenAmount * -1;
            wallet.TokenAmount += amount;
            wallet.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }

    public interface IWalletRepository
    {
        Task AddTransaction(Wallet wallet, PaymentTransaction transaction);
    }
}
