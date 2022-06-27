using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Repositories.Extensions
{
    public static class RequestExtensions
    {
        public static IQueryable<ArticleRequest> WithPledges(this IQueryable<ArticleRequest> request)
        {
            return request.Include(x => x.Pledges)
                .ThenInclude(x => x.Pledger);
        }

        public static IQueryable<ArticleRequest> WithTargetAuthor(this IQueryable<ArticleRequest> request)
        {
            return request.Include(x => x.TargetAuthor);
        }
    }
}

