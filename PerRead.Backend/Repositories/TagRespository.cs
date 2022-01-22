using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Constants;
using PerRead.Backend.Models.BackEnd;
using System.Linq.Expressions;

namespace PerRead.Backend.Repositories
{
    public class TagRespository : ITagRepository
    {
        private readonly AppDbContext _dbContext;

        public TagRespository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tag> GetOrCreate(string tagName)
        {
            var existing = _dbContext.Tags.FirstOrDefault(t => t.TagName == tagName);

            if (existing != null)
            {
                return existing;
            }

            var newTag = new Tag
            {
                TagName = tagName,
                FirstUsage = DateTime.Now
            };

            _dbContext.Tags.Add(newTag);
            await _dbContext.SaveChangesAsync();

            return newTag;
        }

        public IQueryable<Tag> GetTabByTagId(int tagId, int top = BusinessContants.DefaultArticlesWithTag, int startIndex = 0)
        {
            return GetWhereWithArticles(x => x.TagId == tagId);
                //.Skip(startIndex)
                //.Take(top);
        }

        public IQueryable<Tag> GetTagByName(string tagName, int top = BusinessContants.DefaultArticlesWithTag, int startIndex = 0)
        {
            return GetWhereWithArticles(x => x.TagName == tagName);
                //.Skip(startIndex)
                //.Take(top);
        }

        private IQueryable<Tag> GetWhereWithArticles(Expression<Func<Tag, bool>> expression)
        {
            return _dbContext.Tags.AsNoTracking()
                .Where(expression)
                .Include(x => x.Articles)
                    .ThenInclude(art => art.ArticleAuthors)
                    .ThenInclude(aa => aa.Author);
        }
    }

    public interface ITagRepository
    {
        IQueryable<Tag> GetTabByTagId(int tagId, int top = BusinessContants.DefaultArticlesWithTag, int startIndex = 0);

        IQueryable<Tag> GetTagByName(string tagName, int top = BusinessContants.DefaultArticlesWithTag, int startIndex = 0);

        Task<Tag> GetOrCreate(string tagName);
    }

}
