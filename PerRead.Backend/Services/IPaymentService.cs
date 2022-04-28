using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Extensions;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IArticleRepository _articleRepository;

        public PaymentService(IAuthorRepository authorRepository, IHttpContextAccessor accessor, IArticleRepository articleRepository)
        {
            _authorRepository = authorRepository;
            _accessor = accessor;
            _articleRepository = articleRepository;
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
    }

    public interface IPaymentService
    {
        // TODO - the payments service should not be concerned with articleIds
        Task<PaymentResult> Settle(string articleId, string to, long amount);
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

