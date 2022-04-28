using System;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class SectionExtensions
    {
        public static FESectionWithArticles ToFESectionWithArticles(this Section section, Author requester)
        {
            return new FESectionWithArticles
            {
                Name = section.Name,
                Description = section.Description,
                SectionId = section.SectionId,
                ArticlePreviews = section.Articles?.OrderByDescending(a => a.Article.CreatedAt).Select(x => x.Article.ToFEArticlePreview(requester)),
            };
        }

        public static FESectionPreview ToFESectionPreview(this Section section)
        {
            return new FESectionPreview
            {
                Name = section.Name,
                SectionId = section.SectionId
            };
        }
    }
}

