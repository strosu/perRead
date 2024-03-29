﻿using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Repositories.Extensions
{
    public static class ArticleQueryExtensions
    {
        //public static IQueryable<Article> IncludeAuthors(this IQueryable<Article> query)
        //{
        //    return query.Include(article => article.ArticleAuthors)
        //            .ThenInclude(articleAuthor => articleAuthor.Author);
        //}

        public static IQueryable<Article> IncludeTags(this IQueryable<Article> query)
        {
            return query.Include(a => a.Tags);
        }

        public static IQueryable<Article> IncludeSections(this IQueryable<Article> query)
        {
            return query.Include(a => a.Sections)
                    .ThenInclude(s => s.Section);
        }

        public static IQueryable<Article> IncludeOwners(this IQueryable<Article> query)
        {
            return query.Include(x => x.AuthorsLink)
                .ThenInclude(x => x.Author);
        }

        public static IQueryable<Article> IncludeReviews(this IQueryable<Article> query)
        {
            return query.Include(x => x.Reviews);
        }

        public static IQueryable<Article> IfEligible(this IQueryable<Article> query, string requesterId)
        {
            return query.Where(x => !x.VisibleOnlyToOwners || x.AuthorsLink.Select(link => link.AuthorId).Contains(requesterId));
        }

    }
}

