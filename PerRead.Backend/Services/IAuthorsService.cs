using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IRequesterGetter _requesterGetter;

        public AuthorsService(IAuthorRepository authorRepository, IRequesterGetter requesterGetter, IFeedRepository feedRepository)
        {
            _authorRepository = authorRepository;
            _requesterGetter = requesterGetter;
        }

        public async Task<FEAuthor?> GetAuthorAsync(string id)
        {
            var author = _authorRepository.GetAuthorWithArticles(id);

            var requester = await _requesterGetter.GetRequester();


            return await author.Select(x => x.ToFEAuthor(requester)).FirstOrDefaultAsync();
        }

        public async Task<FEAuthor?> GetAuthorByNameAsync(string name)
        {
            var author = _authorRepository.GetAuthorByName(name);

            var requester = await _requesterGetter.GetRequester();
            return await author.Select(x => x.ToFEAuthor(requester)).FirstOrDefaultAsync();
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
    }
}

