using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<FEAuthor?> GetAuthorAsync(string id)
        {
            var author = _authorRepository.GetAuthorWithArticles(id);

            return await author.Select(x => x.ToFEAuthor()).FirstOrDefaultAsync();
        }

        //public async Task<FEAuthor?> GetAuthorByNameAsync(string name)
        //{
        //    var author = _authorRepository.GetAuthorByName(name);
        //    return await author.Select(x => x.ToFEAuthor(requester)).FirstOrDefaultAsync();
        //}

        public async Task<IEnumerable<FEAuthorPreview>> GetAuthorsAsync()
        {
            var authors = _authorRepository.GetAuthors();
            return await authors.Select(x => x.ToFEAuthorPreview()).ToListAsync();
        }
    }

    public interface IAuthorsService
    {
        Task<FEAuthor?> GetAuthorAsync(string id);

        //Task<FEAuthor?> GetAuthorByNameAsync(string name);

        Task<IEnumerable<FEAuthorPreview>> GetAuthorsAsync();
    }
}

