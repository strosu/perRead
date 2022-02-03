using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Extensions;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IHttpContextAccessor _accessor;

        public PaymentService(IAuthorRepository authorRepository, IHttpContextAccessor accessor)
        {
            _authorRepository = authorRepository;
            _accessor = accessor;
        }

        public async Task<PaymentResult> Settle(string to, long amount)
        {
            // Short circuit for free articles
            if (amount == 0)
            {
                return new PaymentResult
                {
                    Result = PaymentResultEnum.Success
                };
            }

            // TODO - this should be done in a single transaction, obviously
            var requester = _accessor.GetUserId();

            var fromAuthor = await _authorRepository.GetAuthor(requester).SingleAsync();

            if (fromAuthor.ReadingTokens < amount)
            {
                return new PaymentResult
                {
                    Result = PaymentResultEnum.Failed,
                    Reason = "not enough money"
                };
            }

            // This should be atomic
            await _authorRepository.AddTokens(requester, -amount);
            await _authorRepository.AddTokens(to, amount);

            return new PaymentResult
            {
                Result = PaymentResultEnum.Success
            };
        }
    }

    public interface IPaymentService
    {
        Task<PaymentResult> Settle(string to, long amount);
    }

    public class PaymentResult
    {
        public PaymentResultEnum Result;

        public string? Reason;
    }

    // TODO - rename this
    public enum PaymentResultEnum
    {
        Success = 0,
        Failed = 1
    }
}

