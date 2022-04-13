using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Helpers
{
    public static class ArticleExtensions
    {
        public static ReadingState ComputeReadingState(this FEArticlePreview articlePreview, Author requester)
        {
            if (articlePreview.AuthorPreviews.Any(x => x.AuthorId == requester.AuthorId))
            {
                return ReadingState.Purchased;
            }

            var articlePrice = articlePreview.ArticlePrice;

            if (requester.ReadingTokens < articlePrice)
            {
                return ReadingState.Unaffordable;
            }

            if (articlePrice <= requester.RequireConfirmationAbove)
            {
                return ReadingState.WithinBuyingLimit;
            }

            return ReadingState.OutsideOfLimitButAffordable;
        }
    }
}
