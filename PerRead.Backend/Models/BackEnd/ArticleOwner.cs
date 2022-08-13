using System;
namespace PerRead.Backend.Models.BackEnd
{
    public class ArticleOwner
    {
        public string ArticleId { get; set; }

        public Article Article { get; set; }

        public string AuthorId { get; set; }

        public Author Author { get; set; }

        public double OwningPercentage { get; set; }

        public bool CanBeRemoved { get; set; }

        public bool IsUserFacing { get; set; }
    }
}

