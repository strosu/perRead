using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Helpers
{
    public static class AuthorHelpers
    {
        public static bool Owns(this Author requester, Article article)
        {
            return article.AuthorsLink.Any(x => x.AuthorId == requester.AuthorId);
        }

        public static bool HasUnlockedArticle(this Author requester, Article article)
        {
            return requester.UnlockedArticles.Any(x => x.ArticleId == article.ArticleId);
        }
    }
}
