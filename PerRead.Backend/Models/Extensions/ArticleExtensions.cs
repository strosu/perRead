using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Models.Helpers;

namespace PerRead.Backend.Models.Extensions
{
    public static class ArticleExtensions
    {
        public static FEArticle ToFEArticle(this Article articleModel)
        {
            return new FEArticle
            {
                Id = articleModel.ArticleId,
                Title = articleModel.Title,
                Content = articleModel.Content,
                AuthorPreviews = articleModel.ArticleAuthors?.Select(x => x.Author.ToFEAuthorPreview()),
                TagPreviews = articleModel.Tags?.Select(s => s.ToFETagPreview()),
                CreatedAt = articleModel.CreatedAt,
                Price = articleModel.Price,
                ArticleImageUrl = string.IsNullOrEmpty(articleModel.ImageUrl) ? "m7kgwe2gnrd81.jpg" : articleModel.ImageUrl,
                SectionPreviews = articleModel.Sections?.Select(s => s?.Section?.ToFESectionPreview())
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
                AuthorPreviews = articleModel.ArticleAuthors?.Select(author => author.Author.ToFEAuthorPreview()),
                TagPreviews = articleModel.Tags?.Select(tag => tag.ToFETagPreview()),
                ArticleImageUrl = string.IsNullOrEmpty(articleModel.ImageUrl) ? "m7kgwe2gnrd81.jpg" : articleModel.ImageUrl,
                ReadingState = articleModel.Price == 0 ? ReadingState.Purchased : ReadingState.Unaffordable,
                SectionPreviews = articleModel.Sections?.Select(s => s.Section.ToFESectionPreview())
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
    }
}
