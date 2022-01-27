using PerRead.Backend.Constants;

namespace PerRead.Backend.Models.Commands
{
    public class ArticleCommand
    {
        public string Title { get; set; }

        //public IEnumerable<string> Authors { get; set; }

        public uint Price { get; set; }

        public string Content { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public ArticleImage ArticleImage { get; set; }
    }

    public class ArticleImage
    {
        public string FileName { get; set; }

        public string Base64Encoded { get; set; }
    }

    internal static class ArticleCommandExtensions
    {
        public static void CheckValid(this ArticleCommand article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            // Business logic
            if (article.Title == null)
            {
                throw new ArgumentNullException(nameof(article.Title));
            }

            // Allow 0 cost articles as free

            //if (Price == 0)
            //{
            //    throw new ArgumentNullException(nameof(Price));
            //}

            if (article.Tags == null || !article.Tags.Any())
            {
                throw new ArgumentException("Each article requires at least one tag");
            }

            if (article.Content == null || article.Content.Length < BusinessContants.MinimumArticleContentLength)
            {
                throw new ArgumentException($"Content must be at least {BusinessContants.MinimumArticleContentLength}");
            }
        }
    }
}
