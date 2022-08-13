using System;
namespace PerRead.Backend.Models.BackEnd
{
    public class ArticleOwner
    {
        public string ArticleId { get; set; }

        public string AuthorId { get; set; }

        public double OwningPercentage { get; set; }
    }
}

