using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(AuthorCommand command)
        {
            var newAuthor = new Author
            {
                AuthorId = command.Id,
                Name = command.Name
            };

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();
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

        public IQueryable<Author> GetAuthorWithArticles(string id)
        {
            return GetAuthor(id)
                .Include(author => author.Articles)
                    .ThenInclude(articleAuthor => articleAuthor.Article)
                    .ThenInclude(article => article.Tags);
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
                .Include(x => x.Articles)
                    .ThenInclude(al => al.Article)
                    .ThenInclude(ar => ar.Tags);
        }

        public async Task<long> AddMoreTokensAsync(string userId)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == userId);

            if (author == null)
            {
                throw new ArgumentException(nameof(userId));
            }

            author.ReadingTokens += 10;

            await _context.SaveChangesAsync();

            return author.ReadingTokens;
        }

        public async Task AddTokens(string authorId, long amount)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == authorId);

            author.ReadingTokens += amount;

            await _context.SaveChangesAsync();
        }
    }

    public interface IAuthorRepository
    {
        Task CreateAsync(AuthorCommand command);

        IQueryable<Author> GetAuthorWithArticles(string id);

        IQueryable<Author> GetAuthor(string id);

        IQueryable<Author> GetAuthorByName(string name);

        IQueryable<Author> GetAuthors();

        Task<long> AddMoreTokensAsync(string usedId);

        Task AddTokens(string authorId, long amount);
    }

    public class AuthorCommand
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}

