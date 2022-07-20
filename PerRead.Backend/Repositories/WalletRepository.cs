﻿using PerRead.Backend.Models.BackEnd;

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
            wallet.TokenAmount += transaction.TokenAmount;
            wallet.OutgoingTransactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }

    public interface IWalletRepository
    {
        Task AddIncomingTransaction(Wallet wallet, PaymentTransaction transaction);

        Task AddOutgoingTransaction(Wallet wallet, PaymentTransaction transaction);
    }
}