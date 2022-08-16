using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.Useful;
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


            newArticle.AuthorsLink = new List<ArticleOwner>
            {
                new ArticleOwner
                {
                    Article = newArticle,
                    Author = author,
                    CanBeEdited = true, // The initial author cannot be removed
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
            return GetAll().Where(x => x.AuthorsLink.Any(a => a.AuthorId == authorId && a.IsUserFacing))
                .OrderByDescending(x => x.CreatedAt).Take(20);
        }

        public async Task<Article?> GetSimpleArticle(string id)
        {
            return await _context.Articles
                .AsNoTracking().FirstOrDefaultAsync(x => x.ArticleId == id);
        }

        public async Task<Article?> GetWithOwners(string id, bool withTracking = false)
        {
            var query = _context.Articles
                .Where(x => x.ArticleId == id);

            if (!withTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.IncludeOwners()
                .SingleAsync();
        }

        public async Task<Article> UpdateOwners(Article article, IEnumerable<AuthorOwnership> owners)
        {
            var ownersToRemove = article.AuthorsLink.Where(x => !owners.Any(y => y.Author.AuthorId == x.AuthorId));

            foreach (var toRemove in ownersToRemove)
            {
                article.AuthorsLink.Remove(toRemove);
            }

            var newOwners = new List<ArticleOwner>();

            foreach (var owner in owners)
            {
                var previousValue = article.AuthorsLink.FirstOrDefault(x => x.AuthorId == owner.Author.AuthorId);
                if (previousValue == null)
                {
                    article.AuthorsLink.Add(new ArticleOwner
                    {
                        AuthorId = owner.Author.AuthorId,
                        ArticleId = article.ArticleId,
                        CanBeEdited = owner.CanBeEdited,
                        IsUserFacing = owner.IsUserFacing,
                        OwningPercentage = owner.Ownership
                    });
                }
                else
                {
                    previousValue.OwningPercentage = owner.Ownership;
                }
            }

            await _context.SaveChangesAsync();

            return await _context.Articles
                .Include(x => x.AuthorsLink).FirstAsync(x => x.ArticleId == article.ArticleId);
        }
    }
}

public interface IArticleRepository
{
    // TODO - extract parameters to something else
    Task<Article> Create(Author author, IEnumerable<Tag> tags, IEnumerable<Section> sections, string articleImagePath, CreateArticleCommand article);

    IQueryable<Article> GetAll();

    Task<Article?> Get(string id);

    Task<Article?> GetWithOwners(string id, bool withTracking = false);

    Task Delete(string id);

    Task<Article?> GetSimpleArticle(string id);

    IQueryable<Article> GetLatestArticles(string authorId);

    Task<Article> UpdateOwners(Article article, IEnumerable<AuthorOwnership> owners);
}
