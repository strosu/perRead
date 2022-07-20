using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Helpers
{
    public static class ArticleExtensions
    {
        public static ReadingState ComputeReadingState(this FEArticlePreview articlePreview, Author requester)
        {
            // If the user is an author, they get to read them for free
            if (articlePreview.AuthorPreviews.Any(x => x.AuthorId == requester.AuthorId))
            {
                return ReadingState.Purchased;
            }

            // If the user already purchased the article, all is well
            if (requester.UnlockedArticles.Any(x => x.ArticleId == articlePreview.ArticleId))
            {
                return ReadingState.Purchased;
            }

            var articlePrice = articlePreview.ArticlePrice;

            //if (requester.MainWallet.TokenAmount < articlePrice)
            //{
            //    return ReadingState.Unaffordable;
            //}

            if (articlePrice <= requester.RequireConfirmationAbove)
            {
                return ReadingState.WithinBuyingLimit;
            }

            return ReadingState.OutsideOfLimitButAffordable;
        }
    }
}
