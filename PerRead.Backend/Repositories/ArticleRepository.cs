using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Repositories.Extensions;

namespace PerRead.Backend.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Article> Create(Author author, IEnumerable<Tag> tags, IEnumerable<Section> sections, string articleImagePath, ArticleCommand article)
        {
            var now = DateTime.UtcNow;

            // Add the article
            var newArticle = new Article
            {
                ArticleId = Guid.NewGuid().ToString(),
                Title = article.Title,
                Price = article.Price,
                Content = article.Content,
                CreatedAt = now,
                ImageUrl = articleImagePath,
            };

            newArticle.Sections = sections.Select(section => new SectionArticle
            {
                Article = newArticle,
                SectionId = section.SectionId
            }).ToList();

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

        public async Task Delete(string id)
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

        public async Task<Article?> Get(string id)
        {
            return await _context.Articles
                .AsNoTracking()
                .Where(x => x.ArticleId == id)
                .IncludeAuthors()
                .IncludeTags()
                .IncludeSections()
                .SingleOrDefaultAsync();
        }

        public IQueryable<Article> GetAll()
        {
            return _context.Articles
                .AsNoTracking()
                .IncludeAuthors()
                .IncludeSections()
                .IncludeTags();
        }

        public IQueryable<Article> GetLatestArticles(string authorId)
        {
            return GetAll().Where(x => x.ArticleAuthors.Any(a => a.AuthorId == authorId))
                .OrderByDescending(x => x.CreatedAt).Take(20);
        }

        public async Task<Article?> GetSimpleArticle(string id)
        {
            return await _context.Articles.FirstOrDefaultAsync(x => x.ArticleId == id);
        }
    }
}

public interface IArticleRepository
{
    // TODO - extract parameters to something else
    Task<Article> Create(Author author, IEnumerable<Tag> tags, IEnumerable<Section> sections, string articleImagePath, ArticleCommand article);

    IQueryable<Article> GetAll();

    Task<Article?> Get(string id);

    Task Delete(string id);

    Task<Article?> GetSimpleArticle(string id);

    IQueryable<Article> GetLatestArticles(string authorId);
}
