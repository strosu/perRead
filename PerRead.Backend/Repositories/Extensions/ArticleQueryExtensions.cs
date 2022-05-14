using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Repositories.Extensions
{
    public static class ArticleQueryExtensions
    {
        public static IQueryable<Article> IncludeAuthors(this IQueryable<Article> query)
        {
            return query.Include(article => article.ArticleAuthors)
                    .ThenInclude(articleAuthor => articleAuthor.Author);
        }

        public static IQueryable<Article> IncludeTags(this IQueryable<Article> query)
        {
            return query.Include(a => a.Tags);
        }

        public static IQueryable<Article> IncludeSections(this IQueryable<Article> query)
        {
            return query.Include(a => a.Sections)
                    .ThenInclude(s => s.Section);
        }
    }
}

