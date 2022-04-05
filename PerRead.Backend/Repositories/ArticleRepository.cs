using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Commands;

namespace PerRead.Backend.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Article> Create(Author author, IEnumerable<Tag> tags, string articleImagePath, ArticleCommand article)
        {
            var now = DateTime.UtcNow;

            // Add the article
            var newArticle = new Article
            {
                Title = article.Title,
                Price = article.Price,
                Content = article.Content,
                CreatedAt = now,
                ImageUrl = articleImagePath
            };

            newArticle.ArticleAuthors = new List<ArticleAuthor>
            {
                new ArticleAuthor
                {
                    Article = newArticle,
                    Author = author,
                    Order = 1
            }
            };

            newArticle.Tags = tags.ToList();

            _context.Articles.Add(newArticle);
            //_context.Entry(author).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();

            return newArticle;
        }

        public async Task Delete(int id)
        {
            var article = await _context.Articles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ArticleId == id);

            if (article == null)
            {
                // No article found, nothing to delete
                return;
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }

        public async Task<Article?> Get(int id)
        {
            return await _context.Articles
                .AsNoTracking()
                .Where(x => x.ArticleId == id)
                .Include(article => article.ArticleAuthors)
                    .ThenInclude(articleAuthor => articleAuthor.Author)
                .Include(a => a.Tags)
                .SingleOrDefaultAsync();
        }

        public IQueryable<Article> GetAll()
        {
            return _context.Articles
                .AsNoTracking()
                .Include(x => x.ArticleAuthors)
                    .ThenInclude(al => al.Author)
                .Include(x => x.Tags);
        }

        public async Task<Article?> GetSimpleArticle(int id)
        {
            return await _context.Articles.FirstOrDefaultAsync(x => x.ArticleId == id);
        }
    }
}

public interface IArticleRepository
{
    // TODO - extract parameters to something else
    Task<Article> Create(Author author, IEnumerable<Tag> tags, string articleImagePath, ArticleCommand article);

    IQueryable<Article> GetAll();

    Task<Article?> Get(int id);

    Task Delete(int id);

    Task<Article?> GetSimpleArticle(int id);
}
