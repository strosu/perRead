using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Extensions;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IArticleRepository _articleRepository;
        private IRequesterGetter _requesterGetter;

        public PaymentService(IAuthorRepository authorRepository, IHttpContextAccessor accessor, IArticleRepository articleRepository, IRequesterGetter requesterGetter)
        {
            _authorRepository = authorRepository;
            _accessor = accessor;
            _articleRepository = articleRepository;
            _requesterGetter = requesterGetter;
        }

        public async Task<PaymentResult> MoveFromEscrow(long amount)
        {
            var validationResult = await TryValidateInput(amount, author => author.EscrowTokens);

            if (!validationResult.opResult)
            {
                return validationResult.result;
            }

            var requester = await _requesterGetter.GetRequester();

            await _authorRepository.MoveFromEscrow(requester, amount);

            return PaymentResult.Success;
        }

        public async Task<PaymentResult> MoveToEscrow(long amount)
        {
            var validationResult = await TryValidateInput(amount, author => author.ReadingTokens);

            if (!validationResult.opResult)
            {
                return validationResult.result;
            }

            var requester = await _requesterGetter.GetRequester();
            await _authorRepository.MoveToEscrow(requester, amount);

            return PaymentResult.Success;
        }

        public async Task<PaymentResult> Settle(string articleId, string to, long amount)
        {
            // Short circuit for free articles
            if (amount == 0)
            {
                return PaymentResult.Success;
            }

            // TODO - this should be done in a single transaction, obviously
            var requester = _accessor.GetUserId();

            // If the author is requesting one of his own articles, just allow
            if (requester == to)
            {
                return PaymentResult.Success;
            }

            var fromAuthor = await _authorRepository.GetAuthorWithReadArticles(requester).SingleAsync();

            // If the current user has already unlocked the article, just let them read it
            if (fromAuthor.UnlockedArticles.Any(x => x.ArticleId == articleId))
            {
                return PaymentResult.Success;
            }

            if (fromAuthor.ReadingTokens < amount)
            {
                return new PaymentResult
                {
                    Result = PaymentResultEnum.Failed,
                    Reason = "not enough money"
                };
            }

            // TODO - this does not belong here
            var article = await _articleRepository.GetSimpleArticle(articleId);

            // This should be atomic
            await _authorRepository.MarkAsRead(requester, article);
            await _authorRepository.AddTokens(to, amount);

            return new PaymentResult
            {
                Result = PaymentResultEnum.Success
            };
        }

        private async Task<(bool opResult, PaymentResult result)> TryValidateInput(long amount, Func<Author, long> func)
        {
            if (amount < 0)
            {
                return (false, new PaymentResult
                {
                    Result = PaymentResultEnum.Failed,
                    Reason = "Invalid amount"
                });
            }

            var requester = _accessor.GetUserId();
            var author = await _authorRepository.GetAuthorWithReadArticles(requester).SingleAsync();

            if (func(author) < amount)
            {
                return (false, new PaymentResult
                {
                    Reason = $"Insufficient funds, please use a value no more than {author.ReadingTokens}",
                    Result = PaymentResultEnum.Failed
                });
            }

            return (true, PaymentResult.Success);
        }
    }

    public interface IPaymentService
    {
        // TODO - the payments service should not be concerned with articleIds
        Task<PaymentResult> Settle(string articleId, string to, long amount);

        Task<PaymentResult> MoveToEscrow(long amount);

        Task<PaymentResult> MoveFromEscrow(long amount);
    }

    public class PaymentResult
    {
        public static PaymentResult Success { get; set; } = new PaymentResult
        {
            Result = PaymentResultEnum.Success
        };


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

