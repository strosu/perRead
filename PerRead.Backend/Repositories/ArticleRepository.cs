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

        public async Task<Article> Create(Author author, IEnumerable<Tag> tags, IEnumerable<Section> sections, string articleImagePath, CreateArticleCommand article)
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


            newArticle.ArticleOwners = new List<ArticleOwner>
            {
                new ArticleOwner
                {
                    Article = newArticle,
                    Author = author,
                    CanBeRemoved = false, // The initial author cannot be removed
                    IsUserFacing = true,
                    OwningPercentage = 1 // Initially the main author owns the article in its entirety
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
                .IncludeOwners()
                .IncludeTags()
                .IncludeSections()
                .SingleOrDefaultAsync();
        }

        public IQueryable<Article> GetAll()
        {
            return _context.Articles
                .AsNoTracking()
                .IncludeOwners()
                .IncludeSections()
                .IncludeTags();
        }

        public IQueryable<Article> GetLatestArticles(string authorId)
        {
            return GetAll().Where(x => x.PublicAuthors.Any(a => a.AuthorId == authorId))
                .OrderByDescending(x => x.CreatedAt).Take(20);
        }

        public async Task<Article?> GetSimpleArticle(string id)
        {
            return await _context.Articles.FirstOrDefaultAsync(x => x.ArticleId == id);
        }

        public async Task<Article?> GetWithOwners(string id)
        {
            return await _context.Articles.
                AsNoTracking()
                .Where(x => x.ArticleId == id)
                .IncludeOwners()
                .SingleAsync();
        }
    }
}

public interface IArticleRepository
{
    // TODO - extract parameters to something else
    Task<Article> Create(Author author, IEnumerable<Tag> tags, IEnumerable<Section> sections, string articleImagePath, CreateArticleCommand article);

    IQueryable<Article> GetAll();

    Task<Article?> Get(string id);

    Task<Article?> GetWithOwners(string id);

    Task Delete(string id);

    Task<Article?> GetSimpleArticle(string id);

    IQueryable<Article> GetLatestArticles(string authorId);
}
