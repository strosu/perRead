using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Models.FrontEnd
{
    public static class FrontEndModelExtensions
    {
        public static FEUserPreview ToUserPreview(this Author author)
        {
            // Check how we can load only part of the row when needed. No point getting all the data if we do a small transform on it
            return new FEUserPreview
            {
                UserId = author.AuthorId,
                UserName = author.Name,
                ProfileImageUri = string.IsNullOrEmpty(author.ProfileImageUri) ? "m7kgwe2gnrd81.jpg" : author.ProfileImageUri,
                ReadingTokens = author.ReadingTokens
            };
        }

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
                ArticlePreviews = authorModel.Articles?.Select(x => x.Article.ToFEArticlePreview()),
                AuthorImageUri = string.IsNullOrEmpty(authorModel.ProfileImageUri) ? "m7kgwe2gnrd81.jpg" : authorModel.ProfileImageUri
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
                AuthorName = authorModel.Name,
                AuthorImageUri = string.IsNullOrEmpty(authorModel?.ProfileImageUri) ? "m7kgwe2gnrd81.jpg" : authorModel.ProfileImageUri
            };
        }

        public static FEAuthorPreview ToFEAuthorPreview(this ArticleAuthor articleAuthorLink)
        {
            return articleAuthorLink.Author.ToFEAuthorPreview();
        }

        public static FEFeedPreview ToFEFeedPreview(this Feed feed)
        {
            return new FEFeedPreview
            {
                FeedId = feed.FeedId,
                FeedName = feed.FeedName,
            };
        }

        public static FEFeed ToFEFeed(this Feed feed, IEnumerable<FEArticlePreview> articles = null)
        {
            // TODO - mayybe revisit passing null as a default
            return new FEFeed
            {
                FeedId = feed.FeedId,
                FeedName = feed.FeedName,
                ArticlePreviews = articles
            };
        }
    }
}
