using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models;
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

        public async Task<Author> GetAuthorAsync(string id)
        {
            var author = _authorRepository.GetAuthorAsync(id);

            return await author?.Select(x => x.ToAuthor())?.FirstOrDefaultAsync();
        }

        public async Task<Author> GetAuthorByNameAsync(string name)
        {
            var author = _authorRepository.GetAuthorByNameAsync(name);
            return await author?.Select(x => x.ToAuthor()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AuthorPreview>> GetAuthorsAsync()
        {
            var authors = _authorRepository.GetAuthors();

            return await authors.Select(x => x.ToAuthorPreivew()).ToListAsync();
        }
    }

    public interface IAuthorsService
    {
        Task<Author> GetAuthorAsync(string id);

        Task<Author> GetAuthorByNameAsync(string name);

        Task<IEnumerable<AuthorPreview>> GetAuthorsAsync();
    }
}

