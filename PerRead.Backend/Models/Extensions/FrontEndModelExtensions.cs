using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Models.FrontEnd
{
    public static class FrontEndModelExtensions
    {
        public static FEArticle ToArticle(this Article articleModel)
        {
            return new FEArticle 
            {
                Id = articleModel.ArticleId,
                Title = articleModel.Title,
                Content = articleModel.Content,
                AuthorPreviews = articleModel.ArticleAuthors.Select(x => x.Author.ToAuthorPreivew()),
                TagPreviews = articleModel.Tags.Select(ToTagPreview)
            };
        }

        public static FEArticlePreview ToArticlePreview(this Article articleModel)
        {
            return new FEArticlePreview
            {
                ArticleId = articleModel.ArticleId,
                ArticleTitle = articleModel.Title,
                ArticleCreatedAt = articleModel.CreatedAt,
                //Preview = articleModel.Preview;
            };
        }

        public static FETag ToTag(this Tag tagModel) 
        {
            return new FETag
            {
                Id = tagModel.TagId,
                Name = tagModel.TagName,
                ArticlePreviews = tagModel.Articles.Select(ToArticlePreview),
                FirstUsage = tagModel.FirstUsage,
            };
        }

        public static FETagPreview ToTagPreview(this Tag tagModel)
        {
            return new FETagPreview
            {
                TagId = tagModel.TagId,
                TagName = tagModel.TagName
            };
        }

        public static FEAuthor ToAuthor(this Author authorModel)
        {
            return new FEAuthor
            {
                Id = authorModel.AuthorId,
                Name = authorModel.Name,
                ArticlePreviews = authorModel.Articles.Select(x => x.Article.ToArticlePreview())
            };
        }

        public static FEAuthorPreview ToAuthorPreivew(this Author authorModel)
        {
            return new FEAuthorPreview
            {
                AuthorId = authorModel.AuthorId,
                AuthorName = authorModel.Name
            };
        }
    }
}
