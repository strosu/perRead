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

        public IQueryable<Author> GetAuthorAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentException(nameof(id));
            }

            return _context.Authors.AsNoTracking()
                .Where(x => x.AuthorId == id);
        }

        public IQueryable<Author> GetAuthorByNameAsync(string name)
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
    }

    public interface IAuthorRepository
    {
        Task CreateAsync(AuthorCommand command);

        IQueryable<Author> GetAuthorAsync(string id);

        IQueryable<Author> GetAuthorByNameAsync(string name);

        IQueryable<Author> GetAuthors();
    }

    public class AuthorCommand
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}

