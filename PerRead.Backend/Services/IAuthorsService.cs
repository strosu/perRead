using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IHttpContextAccessor _accessor;

        public AuthorsService(IAuthorRepository authorRepository, IHttpContextAccessor accessor)
        {
            _authorRepository = authorRepository;
            _accessor = accessor;
        }

        public async Task<FEAuthor?> GetAuthorAsync(string id)
        {
            var author = _authorRepository.GetAuthorWithArticles(id);

            return await author.Select(x => x.ToFEAuthor()).FirstOrDefaultAsync();
        }

        public async Task<FEAuthor?> GetAuthorByNameAsync(string name)
        {
            var author = _authorRepository.GetAuthorByName(name);
            return await author.Select(x => x.ToFEAuthor()).FirstOrDefaultAsync();
        }

        public async Task<FEAuthorPreview?> GetCurrentAuthor()
        {
            var authorId = _accessor.GetUserId();

            var author = _authorRepository.GetAuthor(authorId);

            return await author.Select(x => x.ToFEAuthorPreview()).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<FEAuthorPreview>> GetAuthorsAsync()
        {
            var authors = _authorRepository.GetAuthors();

            return await authors.Select(x => x.ToFEAuthorPreview()).ToListAsync();
        }
    }

    public interface IAuthorsService
    {
        Task<FEAuthor?> GetAuthorAsync(string id);

        Task<FEAuthor?> GetAuthorByNameAsync(string name);

        Task<IEnumerable<FEAuthorPreview>> GetAuthorsAsync();

        Task<FEAuthorPreview?> GetCurrentAuthor();
    }
}

