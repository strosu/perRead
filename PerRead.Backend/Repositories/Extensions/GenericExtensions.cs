using Microsoft.EntityFrameworkCore;

namespace PerRead.Backend.Repositories.Extensions
{
    public static class GenericExtensions
    {
        public static IQueryable<TEntity> WithTrackingIfNeeded<TEntity>(this IQueryable<TEntity> query, bool withTracking) where TEntity : class
        {
            if (!withTracking)
            {
                return query.AsNoTracking();
            }

            return query;
        }
    }
}
