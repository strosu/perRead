using System.ComponentModel.DataAnnotations.Schema;

namespace PerRead.Backend.Models.BackEnd
{
    /// <summary>
    /// Article as loaded from the Database
    /// </summary>
    public class Article
    {
        public string ArticleId { get; set; }

        public string Title { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public uint Price { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<SectionArticle> Sections { get; set; }

        public ICollection<ArticleOwner> AuthorsLink { get; set; }

        public bool VisibleOnlyToOwners { get; set; }

        [NotMapped]
        public IEnumerable<ArticleOwner> PublicAuthors => AuthorsLink.Where(x => x.IsUserFacing);
    }
}