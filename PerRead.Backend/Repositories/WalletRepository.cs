﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Wallet> GetWallet(string walledId)
        {
            return await GetWalletQuery(walledId).SingleOrDefaultAsync();
        }

        public IQueryable<Wallet> GetWalletQuery(string walletId)
        {
            return _context.Wallets.Where(x => x.WalledId == walletId);
        }
    }

    public interface IWalletRepository
    {
        Task<Wallet> GetWallet(string walledId);

        IQueryable<Wallet> GetWalletQuery(string walletId);

        Task AddIncomingTransaction(Wallet wallet, PaymentTransaction transaction);

        Task AddOutgoingTransaction(Wallet wallet, PaymentTransaction transaction);
    }
}
