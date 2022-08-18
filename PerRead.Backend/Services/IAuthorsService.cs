using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IRequesterGetter _requesterGetter;

        public AuthorsService(IAuthorRepository authorRepository, IArticleRepository articleRepository, IRequesterGetter requesterGetter)
        {
            _authorRepository = authorRepository;
            _articleRepository = articleRepository;
            _requesterGetter = requesterGetter;
        }

        public async Task<FEAuthor> GetAuthorAsync(string authorId)
        {
            var author = _authorRepository.GetAuthorWithArticles(authorId);

            var authorWithArticles = await author.Select(x => x.ToFEAuthor()).FirstOrDefaultAsync();

            var requester = await _requesterGetter.GetRequesterWithArticles();

            authorWithArticles.LatestArticles = await _articleRepository.GetLatestVisibleArticles(authorId, requester.AuthorId).Select(x => x.ToFEArticlePreview(requester)).ToListAsync();

            return authorWithArticles;
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
        Task<FEAuthor> GetAuthorAsync(string id);

        //Task<FEAuthor?> GetAuthorByNameAsync(string name);

        Task<IEnumerable<FEAuthorPreview>> GetAuthorsAsync();
    }
}

