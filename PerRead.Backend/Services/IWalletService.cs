using PerRead.Backend.Models;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IWalletService
    {
        Task<long> AddTokensForCurrentUser(long amount);

        Task<long> WithdrawTokensForCurrentUser(long amount);

        Task MoveToEscrow(Author author, long amount);

        Task MoveFromEscrow(Author author, long amount);

        Task<TransactionResult> UnlockArticle(Author owner, long amount);

        Task ReleaseInitialPledgeFunds(RequestPledge pledge);

        Task ReleaseRemainingPledgeFunds(RequestPledge pledge);
    }

    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRequesterGetter _requesterGetter;

        //public async Task<long> AddMoreTokens(string authorId, long amount)
        //{
        //    var userId = _accessor.GetUserId();
        //    return await _authorRepository.AddMoreTokensAsync(userId, amount);
        //}

        //public async Task<long> WithdrawTokens(int amount)
        //{
        //    var authorid = _accessor.GetUserId();
        //    return await _authorRepository.WithdrawTokens(authorid, amount);
        //}

        public WalletService(ITransactionRepository transactionRepository, IWalletRepository walletRepository, IRequesterGetter requesterGetter)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
            _requesterGetter = requesterGetter;
        }

        public async Task<long> AddTokensForCurrentUser(long amount)
        {
            var author = await _requesterGetter.GetRequester();
            //await Transact(ModelConstants.CompanyWallet, author.MainWallet, amount, TransactionType.TokenPurchase);
            //return author.MainWallet.TokenAmount; 
            return 0;
        }

        public async Task<long> WithdrawTokensForCurrentUser(long amount)
        {
            var author = await _requesterGetter.GetRequester();
            //await Transact(author.MainWallet, ModelConstants.CompanyWallet, amount, TransactionType.TokenWithdrawal);
            //return author.MainWallet.TokenAmount;

            return 0;
        }

        public async Task MoveToEscrow(Author author, long amount)
        {
            //var author = await _requesterGetter.GetRequester();
            //await Transact(author.MainWallet, author.EscrowWallet, amount, TransactionType.MoveToEscrow);
        }

        public async Task MoveFromEscrow(Author author, long amount)
        {
            //var author = await _requesterGetter.GetRequester();
            //await Transact(author.EscrowWallet, author.MainWallet, amount, TransactionType.MoveFromEscrow);
        }

        public Task ReleaseInitialPledgeFunds(RequestPledge pledge)
        {
            throw new NotImplementedException();
        }

        public Task ReleaseRemainingPledgeFunds(RequestPledge pledge)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionResult> UnlockArticle(Author owner, long amount)
        {
            var buyer = await _requesterGetter.GetRequester();
            //return await Transact(buyer.MainWallet, owner.MainWallet, amount, TransactionType.MoveFromEscrow);
            return null;
        }

        private async Task<TransactionResult> Transact(Wallet from, Wallet to, long amount, TransactionType transactionType)
        {
            var transactionResult = await _transactionRepository.AddTransaction(from, to, amount, transactionType);

            if (transactionResult.Result == PaymentResultEnum.Failed)
            {
                return transactionResult;
            }

            await _walletRepository.AddOutgoingTransaction(from, transactionResult.Transaction);
            await _walletRepository.AddIncomingTransaction(to, transactionResult.Transaction);
            return transactionResult;
        }
    }
}
