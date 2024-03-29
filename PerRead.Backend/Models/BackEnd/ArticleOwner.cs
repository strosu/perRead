﻿using System;
namespace PerRead.Backend.Models.BackEnd
{
    public class ArticleOwner
    {
        public string ArticleId { get; set; }

        public string AuthorId { get; set; }

        public Article Article { get; set; }

        public Author Author { get; set; }

        public double OwningPercentage { get; set; }

        public bool CanBeEdited { get; set; }

        public bool IsUserFacing { get; set; }

        public bool IsPublisher { get; set; }
    }
}