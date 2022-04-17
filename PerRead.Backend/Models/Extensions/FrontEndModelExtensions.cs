using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.Helpers;

namespace PerRead.Backend.Models.FrontEnd
{
    public static class FrontEndModelExtensions
    {
        public static FEArticle ToFEArticle(this Article articleModel)
        {
            return new FEArticle
            {
                Id = articleModel.ArticleId,
                Title = articleModel.Title,
                Content = articleModel.Content,
                AuthorPreviews = articleModel.ArticleAuthors?.Select(x => x.Author.ToFEAuthorPreview()),
                TagPreviews = articleModel.Tags?.Select(ToFETagPreview),
                CreatedAt = articleModel.CreatedAt,
                Price = articleModel.Price,
                ArticleImageUrl = string.IsNullOrEmpty(articleModel.ImageUrl) ? "m7kgwe2gnrd81.jpg" : articleModel.ImageUrl
            };
        }

        public static FEArticlePreview ToFEArticlePreview(this Article articleModel, Author requester = null)
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
                ReadingState = articleModel.Price == 0 ? ReadingState.Purchased : ReadingState.Unaffordable
            };

            if (requester != null)
            {
                articlePreview.ReadingState = articlePreview.ComputeReadingState(requester);
            }

            return articlePreview;
        }

        public static FETag ToFETag(this Tag tagModel)
        {
            return new FETag
            {
                Id = tagModel.TagId,
                Name = tagModel.TagName,
                ArticlePreviews = tagModel.Articles?.Select(x => x.ToFEArticlePreview()),
                FirstUsage = tagModel.FirstUsage,
            };
        }

        public static FETagPreview ToFETagPreview(this Tag tagModel)
        {
            return new FETagPreview
            {
                TagId = tagModel.TagId,
                TagName = tagModel.TagName
            };
        }


    }
}
