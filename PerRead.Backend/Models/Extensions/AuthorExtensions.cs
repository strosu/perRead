using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class AuthorExtensions
    {
        public static FEAuthor ToFEAuthor(this Author authorModel)
        {
            return new FEAuthor
            {
                Id = authorModel.AuthorId,
                Name = authorModel.Name,
                AuthorImageUri = string.IsNullOrEmpty(authorModel.ProfileImageUri) ? "m7kgwe2gnrd81.jpg" : authorModel.ProfileImageUri,
                SectionPreviews = authorModel.PublishSections.Select(x => x.ToFESectionPreview()),
                ArticleCount = authorModel.PublishedArticleCount,
                About = authorModel.About
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
                AuthorImageUri = string.IsNullOrEmpty(authorModel?.ProfileImageUri) ? "m7kgwe2gnrd81.jpg" : authorModel.ProfileImageUri,
                ArticleCount = authorModel.PublishedArticleCount
            };
        }

        public static FEAuthorPreview ToFEAuthorPreview(this ArticleAuthor articleAuthorLink)
        {
            return articleAuthorLink.Author.ToFEAuthorPreview();
        }

        public static FEUserSettings ToFEUserSettings(this Author authorModel)
        {
            return new FEUserSettings
            {
                UserName = authorModel.Name,
                RequireConfirmationAbove = authorModel.RequireConfirmationAbove,
                About = authorModel.About
            };
        }

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
    }
}
