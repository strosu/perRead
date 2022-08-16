using Microsoft.EntityFrameworkCore;
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

        public async Task AddIncomingTransaction(Wallet wallet, PaymentTransaction transaction)
        {
            wallet.TokenAmount += transaction.TokenAmount;
            wallet.IncomingTransactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task AddOutgoingTransaction(Wallet wallet, PaymentTransaction transaction)
        {
            wallet.TokenAmount -= transaction.TokenAmount;
            wallet.OutgoingTransactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<Wallet> CreateWallet()
        {
            var wallet = new Wallet
            {
                IncomingTransactions = new List<PaymentTransaction>(),
                OutgoingTransactions = new List<PaymentTransaction>(),
                TokenAmount = 0,
                WalledId = Guid.NewGuid().ToString()
            };

            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();

            return wallet;
        }

        public async Task<Wallet> GetWallet(string walledId)
        {
            return await GetWalletQuery(walledId).SingleOrDefaultAsync();
        }

        public IQueryable<Wallet> GetWalletQuery(string walletId)
        {
            return _context.Wallets.Where(x => x.WalledId == walletId);
        }

        public async Task<Wallet> GetWalletWithTransactions(string walletId)
        {
            return await GetWalletQuery(walletId)
                .Include(x => x.IncomingTransactions)
                    //.ThenInclude(x => x.SourceWallet)
                .Include(x => x.OutgoingTransactions)
                    //.ThenInclude(x => x.DestinationWallet)
                .SingleOrDefaultAsync();
        }
    }

    public interface IWalletRepository
    {
        Task<Wallet> GetWallet(string walledId);

        Task<Wallet> GetWalletWithTransactions(string walletId);

        IQueryable<Wallet> GetWalletQuery(string walletId);

        Task AddIncomingTransaction(Wallet wallet, PaymentTransaction transaction);

        Task AddOutgoingTransaction(Wallet wallet, PaymentTransaction transaction);

        Task<Wallet> CreateWallet();
    }
}
