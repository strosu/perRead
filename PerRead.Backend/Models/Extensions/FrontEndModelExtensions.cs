using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Models.FrontEnd
{
    public static class FrontEndModelExtensions
    {
        public static Article ToArticle(this ArticleModel articleModel)
        {
            return new Article 
            {
                Id = articleModel.ArticleId,
                Title = articleModel.Title,
                Content = articleModel.Content,
                AuthorPreviews = articleModel.ArticleAuthors.Select(x => x.Author.ToAuthorPreivew()),
                TagPreviews = articleModel.Tags.Select(ToTagPreview)
            };
        }

        public static ArticlePreview ToArticlePreview(this ArticleModel articleModel)
        {
            return new ArticlePreview
            {
                Id = articleModel.ArticleId,
                Title = articleModel.Title,
                //Preview = articleModel.Preview;
            };
        }

        public static Tag ToTag(this TagModel tagModel) 
        {
            return new Tag
            {
                TagId = tagModel.TagId,
                TagName = tagModel.TagName,
                ArticlePreviews = tagModel.Articles.Select(ToArticlePreview)
            };
        }

        public static TagPreview ToTagPreview(this TagModel tagModel)
        {
            return new TagPreview
            {
                TagId = tagModel.TagId,
                TagName = tagModel.TagName
            };
        }

        public static Author ToAuthor(this AuthorModel authorModel)
        {
            return new Author
            {
                Id = authorModel.AuthorId,
                Name = authorModel.Name,
                ArticlePreviews = authorModel.Articles.Select(x => x.Article.ToArticlePreview())
            };
        }

        public static AuthorPreview ToAuthorPreivew(this AuthorModel authorModel)
        {
            return new AuthorPreview
            {
                Id = authorModel.AuthorId,
                Name = authorModel.Name
            };
        }
    }
}
