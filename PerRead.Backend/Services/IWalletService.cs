using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;
using System;

namespace PerRead.Backend.Services
{
    public interface IWalletService
    {
        Task<long> AddTokensForCurrentUser(long amount);

        Task<long> WithdrawTokensForCurrentUser(long amount);

        Task MoveToEscrow(Author author, long amount, string requestId);

        Task MoveFromEscrow(Author author, long amount, TransactionType transactionType, string requestId);

        Task<TransactionResult> TransferArticlePriceToCompany(Author buyer, long amount, string articleId);

        Task<TransactionResult> CompanyTransferToAuthor(Author owner, long amount, string articleId);

        Task ReleaseInitialPledgeFunds(RequestPledge pledge);

        Task ReleaseRemainingPledgeFunds(RequestPledge pledge);

        Task ReleaseBackToPledger(RequestPledge pledge);

        Task<FEWallet> GetWallet(string walletId);

        Task<FEWallet> GetCurrentUserMainWallet();
    }

    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRequesterGetter _requesterGetter;

        public WalletService(ITransactionRepository transactionRepository, IWalletRepository walletRepository, IRequesterGetter requesterGetter)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
            _requesterGetter = requesterGetter;
        }

        public async Task<long> AddTokensForCurrentUser(long amount)
        {
            var author = await _requesterGetter.GetRequester();
            var companyWallet = await _walletRepository.GetWallet(ModelConstants.CompanyWalletId);

            await Transact(companyWallet, author.MainWallet, amount, TransactionType.TokenPurchase);
            return author.MainWallet.TokenAmount;
        }

        public async Task<long> WithdrawTokensForCurrentUser(long amount)
        {
            var author = await _requesterGetter.GetRequester();
            var companyWallet = await _walletRepository.GetWallet(ModelConstants.CompanyWalletId);

            await Transact(author.MainWallet, companyWallet, amount, TransactionType.TokenWithdrawal);
            return author.MainWallet.TokenAmount;
        }

        public async Task MoveToEscrow(Author author, long amount, string pledgeId)
        {
            await Transact(author.MainWallet, author.EscrowWallet, amount, TransactionType.PledgeCreated, pledgeId);
        }

        public async Task MoveFromEscrow(Author author, long amount, TransactionType transactionType, string pledgeId)
        {
            //var author = await _requesterGetter.GetRequester();
            await Transact(author.EscrowWallet, author.MainWallet, amount, transactionType, pledgeId);
        }

        public async Task ReleaseInitialPledgeFunds(RequestPledge pledge)
        {
            await ReleasePledge(pledge, pledge.TokensOnAccept, TransactionType.RequestAccepted);
        }

        public async Task ReleaseRemainingPledgeFunds(RequestPledge pledge)
        {
            await ReleasePledge(pledge, pledge.TotalTokenSum - pledge.TokensOnAccept, TransactionType.RequestCompleted);
        }

        public async Task ReleaseBackToPledger(RequestPledge pledge)
        {
            await Transact(pledge.Pledger.EscrowWallet, pledge.Pledger.MainWallet, pledge.TotalTokenSum - pledge.TokensOnAccept, TransactionType.RequestCancelled,
                pledge.ParentRequest.ArticleRequestId);
        }

        private async Task<TransactionResult> ReleasePledge(RequestPledge pledge, long amount, TransactionType transactionType)
        {
            var funderWallet = await _walletRepository.GetWallet(pledge.Pledger.EscrowWalletId);
            var receiverWallet = await _walletRepository.GetWallet(pledge.ParentRequest.TargetAuthor.MainWalletId);

            return await Transact(funderWallet, receiverWallet, amount, transactionType, pledge.RequestPledgeId);
        }

        private async Task<TransactionResult> Transact(Wallet from, Wallet to, long amount, TransactionType transactionType, string? comment = null)
        {
            var transactionResult = await _transactionRepository.AddTransaction(from, to, amount, transactionType, comment);

            if (transactionResult.Result == PaymentResultEnum.Failed)
            {
                return transactionResult;
            }

            await _walletRepository.AddOutgoingTransaction(from, transactionResult.Transaction);
            await _walletRepository.AddIncomingTransaction(to, transactionResult.Transaction);
            return transactionResult;
        }

        public async Task<FEWallet> GetWallet(string walletId)
        {
            return await _walletRepository.GetWalletQuery(walletId).Select(x => x.ToFEWallet()).FirstOrDefaultAsync();
        }

        public async Task<FEWallet> GetCurrentUserMainWallet()
        {
            var curentUser = await _requesterGetter.GetRequester();
            var wallet = await _walletRepository.GetWalletWithTransactions(curentUser.MainWalletId);
            return wallet.ToFEWallet();
        }

        public async Task<TransactionResult> TransferArticlePriceToCompany(Author buyer, long amount, string articleId)
        {
            var buyerWallet = await _walletRepository.GetWalletWithTransactions(buyer.MainWalletId);

            var companyWallet = await _walletRepository.GetWallet(ModelConstants.CompanyWalletId);

            return await Transact(buyerWallet, companyWallet, amount, TransactionType.ArticleRead, articleId);
        }

        public async Task<TransactionResult> CompanyTransferToAuthor(Author owner, long amount, string articleId)
{
            var ownerWallet = await _walletRepository.GetWalletWithTransactions(owner.MainWalletId);

            var companyWallet = await _walletRepository.GetWallet(ModelConstants.CompanyWalletId);

            return await Transact(companyWallet, ownerWallet, amount, TransactionType.ArticleRead, articleId);
        }
    }
}
