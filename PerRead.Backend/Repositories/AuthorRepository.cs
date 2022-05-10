using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Author> CreateAsync(AuthorCommand command)
        {
            var newAuthor = new Author
            {
                AuthorId = command.Id,
                Name = command.Name
            };

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();

            return newAuthor;
        }

        public IQueryable<Author> GetAuthor(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _context.Authors
                .Where(author => author.AuthorId == id);
        }

        public IQueryable<Section> GetAuthorSections(string authorId)
        {
            return GetAuthor(authorId)
                .Include(author => author.PublishSections)
                .SelectMany(x => x.PublishSections);
        }

        public IQueryable<Author> GetAuthorWithArticles(string id)
        {
            return GetAuthor(id)
                .Include(author => author.PublishSections)
                    .ThenInclude(author => author.Articles)
                    .ThenInclude(articleAuthor => articleAuthor.Article)
                    .ThenInclude(article => article.Tags);
        }

        public IQueryable<Author> GetAuthorWithReadArticles(string id)
        {
            return GetAuthor(id)
                .Include(author => author.UnlockedArticles)
                .ThenInclude(articleUnlock => articleUnlock.Article)
                .ThenInclude(article => article.ArticleAuthors)
                .ThenInclude(aa => aa.Author);
        }

        public IQueryable<Author> GetAuthorByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            return _context.Authors.AsNoTracking()
                .Where(x => x.Name == name);
        }

        public IQueryable<Author> GetAuthors()
        {
            return _context.Authors.AsNoTracking()
                .Include(x => x.PublishSections)
                .ThenInclude(x => x.Articles)
                    .ThenInclude(al => al.Article)
                    .ThenInclude(ar => ar.Tags);
        }

        public async Task<long> AddMoreTokensAsync(string userId, int amount)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == userId);

            if (author == null)
            {
                throw new ArgumentException(nameof(userId));
            }

            author.ReadingTokens += amount;

            await _context.SaveChangesAsync();

            return author.ReadingTokens;
        }

        public async Task<long> WithdrawTokens(string authorId, int amount)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == authorId);

            if (author == null)
            {
                throw new ArgumentException(nameof(authorId));
            }

            if (author.ReadingTokens < amount)
            {
                throw new ArgumentException("Insufficient tokens");
            }

            author.ReadingTokens -= amount;

            await _context.SaveChangesAsync();

            return author.ReadingTokens;
        }


        public async Task AddTokens(string authorId, long amount)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == authorId);

            author.ReadingTokens += amount;

            await _context.SaveChangesAsync();
        }

        public async Task MarkAsRead(string authorId, Article article)
        {
            var author = await (_context.Authors.Where(x => x.AuthorId == authorId)
                .Include(x => x.UnlockedArticles)).FirstOrDefaultAsync();

            //var author = await GetAuthorWithReadArticles(authorId).FirstOrDefaultAsync();

            author.UnlockedArticles.Add(new ArticleUnlock
            {
                AquisitionDate = DateTime.UtcNow,
                AquisitionPrice = article.Price,
                ArticleId = article.ArticleId,
                AuthorId = authorId,
            });
            author.ReadingTokens -= article.Price;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateSettings(string authorId, FEUserSettings userSettings)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == authorId);

            author.RequireConfirmationAbove = userSettings.RequireConfirmationAbove;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateReadArticles(string authorId, IEnumerable<long> unlockedIds)
        {
            var author = await _context.Authors
                .Include(x => x.UnlockedArticles).FirstOrDefaultAsync(x => x.AuthorId == authorId);

            var toRemoveList = author.UnlockedArticles.Where(x => !unlockedIds.Contains(x.Id)).ToList();

            foreach (var toRemove in toRemoveList)
            {
                author.UnlockedArticles.Remove(toRemove);
            }

            //var articlesUnlocked = await _context.Authors.Where(x => x.AuthorId == authorId)
            //    .Include(x => x.UnlockedArticles)
            //    .SelectMany(x => x.UnlockedArticles)
            //    .ToListAsync();

            //articlesUnlocked.RemoveAll(x => !unlockedIds.Contains(x.Id));

            await _context.SaveChangesAsync();
        }
    }

    public interface IAuthorRepository
    {
        Task<Author> CreateAsync(AuthorCommand command);

        IQueryable<Author> GetAuthorWithArticles(string id);

        IQueryable<Author> GetAuthorWithReadArticles(string id);

        IQueryable<Author> GetAuthor(string id);

        IQueryable<Author> GetAuthorByName(string name);

        IQueryable<Author> GetAuthors();

        IQueryable<Section> GetAuthorSections(string authorId);

        Task<long> AddMoreTokensAsync(string usedId, int amount);

        Task<long> WithdrawTokens(string authorId, int amount);

        Task AddTokens(string authorId, long amount);

        Task MarkAsRead(string authorId, Article article);

        // TODO - rename userSettings object to something else;
        // Should be fine to use it directly, as it's simply a representation of the data from the DB
        Task UpdateSettings(string authorId, FEUserSettings userSettings);

        Task UpdateReadArticles(string authorId, IEnumerable<long> unlockedIds);
    }

    public class AuthorCommand
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}

