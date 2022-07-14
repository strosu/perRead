using PerRead.Backend.Extensions;

namespace PerRead.Backend.Services
{
    public interface IWalletService
    {
        Task<long> AddMoreTokens(long amount);

        Task<long> WithdrawTokens(long amount);
    }

    public class WalletService : IWalletService
    {
        private readonly IHttpContextAccessor _accessor;

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

        public WalletService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public Task<long> AddMoreTokens(long amount)
        {
            throw new NotImplementedException();
        }

        public Task<long> WithdrawTokens(long amount)
        {
            throw new NotImplementedException();
        }
    }
}
