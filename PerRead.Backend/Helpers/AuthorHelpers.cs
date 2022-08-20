using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Helpers
{
    public static class AuthorHelpers
    {
        public static bool Owns(this Author requester, Article article, out PaymentTransaction purchaseTransaction)
        {
            // TODO - figure out how to use the request completion transaction here
            purchaseTransaction = null;
            var ownsIt = article.AuthorsLink.Any(x => x.AuthorId == requester.AuthorId);

            //var request = article.SourceRequest;
            //if (request != null)
            //{
                //purchaseTransaction = request.Pledges.FirstOrDefault(x => x.Pledger.AuthorId == requester.AuthorId)?
            //}

            return ownsIt;
        }

        public static bool HasUnlockedArticle(this Author requester, Article article)
        {
            return requester.UnlockedArticles.Any(x => x.ArticleId == article.ArticleId);
        }
    }
}
