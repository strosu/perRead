using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class ArticleExtensions
    {
        public static FEArticle ToFEArticle(this Article articleModel, Author requester)
        {
            return new FEArticle
            {
                Id = articleModel.ArticleId,
                Title = articleModel.Title,
                Content = articleModel.Content,
                AuthorPreviews = articleModel.PublicAuthors?.Select(x => x.Author.ToFEAuthorPreview()),
                TagPreviews = articleModel.Tags?.Select(s => s.ToFETagPreview()),
                CreatedAt = articleModel.CreatedAt,
                Price = articleModel.Price,
                ArticleImageUrl = string.IsNullOrEmpty(articleModel.ImageUrl) ? "m7kgwe2gnrd81.jpg" : articleModel.ImageUrl,
                SectionPreviews = articleModel.Sections?.Select(s => s?.Section?.ToFESectionPreview()),
                RecommendsReadingCount = articleModel.RecommendsReadingCount,
                NotRecommendsReadingCount = articleModel.NotRecommendsReadingCount,
                CurrentUserRecommends = articleModel.Reviews?.FirstOrDefault(x => x.AuthorId == requester.AuthorId)?.Recommends
            };
        }

        public static FEArticlePreview ToFEArticlePreview(this Article articleModel, Author requester)
        {
            var articlePreview = new FEArticlePreview
            {
                ArticleId = articleModel.ArticleId,
                ArticleTitle = articleModel.Title,
                ArticleCreatedAt = articleModel.CreatedAt,
                ArticlePreview = "TODO - Add previews",
                ArticlePrice = articleModel.Price,
                AuthorPreviews = articleModel.PublicAuthors?.Select(author => author.Author.ToFEAuthorPreview()),
                TagPreviews = articleModel.Tags?.Select(tag => tag.ToFETagPreview()),
                ArticleImageUrl = string.IsNullOrEmpty(articleModel.ImageUrl) ? "m7kgwe2gnrd81.jpg" : articleModel.ImageUrl,
                ReadingState = articleModel.Price == 0 ? ReadingState.Purchased : ReadingState.Unaffordable,
                SectionPreviews = articleModel.Sections?.Select(s => s.Section.ToFESectionPreview()),
                RecommendsReadingCount = articleModel.RecommendsReadingCount,
                NotRecommendsReadingCount = articleModel.NotRecommendsReadingCount,
            };

            if (requester != null)
            {
                articlePreview.ReadingState = articlePreview.ComputeReadingState(requester);
            }

            return articlePreview;
        }

        public static FEArticleUnlockInfo ToFEArticleUnlockInfo(this ArticleUnlock articleUnlock)
        {
            return new FEArticleUnlockInfo
            {
                Article = articleUnlock.Article.ToFEArticlePreview(Author.NonLoggedInAuthor),
                ArticleUnlockId = articleUnlock.Id,
                AquisitionDate = articleUnlock.AquisitionDate,
                AquisitionPrice = articleUnlock.AquisitionPrice,
            };
        }

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

            if (requester.MainWallet.TokenAmount < articlePrice)
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
