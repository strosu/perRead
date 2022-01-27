using PerRead.Backend.Models.BackEnd;

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
            };
        }

        public static FEArticlePreview ToFEArticlePreview(this Article articleModel)
        {
            return new FEArticlePreview
            {
                ArticleId = articleModel.ArticleId,
                ArticleTitle = articleModel.Title,
                ArticleCreatedAt = articleModel.CreatedAt,
                ArticlePreview = "TODO - Add previews",
                ArticlePrice = articleModel.Price,
                AuthorPreviews = articleModel.ArticleAuthors?.Select(author => author.Author.ToFEAuthorPreview()),
                TagPreviews = articleModel.Tags?.Select(tag => tag.ToFETagPreview()),
                ArticleImageUrl = string.IsNullOrEmpty(articleModel.ImageUrl) ? "m7kgwe2gnrd81.jpg" : articleModel.ImageUrl
            };
        }

        public static FETag ToFETag(this Tag tagModel) 
        {
            return new FETag
            {
                Id = tagModel.TagId,
                Name = tagModel.TagName,
                ArticlePreviews = tagModel.Articles?.Select(ToFEArticlePreview),
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

        public static FEAuthor ToFEAuthor(this Author authorModel)
        {
            return new FEAuthor
            {
                Id = authorModel.AuthorId,
                Name = authorModel.Name,
                ArticlePreviews = authorModel.Articles?.Select(x => x.Article.ToFEArticlePreview())
            };
        }

        public static FEAuthor ToFEAuthor(this ArticleAuthor articleAuthorLink)
        {
            return articleAuthorLink.Author.ToFEAuthor();
        }


        public static FEAuthorPreview ToFEAuthorPreview(this Author authorModel)
        {
            return new FEAuthorPreview
            {
                AuthorId = authorModel.AuthorId,
                AuthorName = authorModel.Name
            };
        }

        public static FEAuthorPreview ToFEAuthorPreview(this ArticleAuthor articleAuthorLink)
        {
            return articleAuthorLink.Author.ToFEAuthorPreview();
        }
    }
}
