using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Extensions;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IRequesterGetter
    {
        Task<Author> GetRequesterWithArticles();
        Task<Author> GetRequester();
    }

    public class RequesterGetter : IRequesterGetter
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IHttpContextAccessor _accessor;

        public RequesterGetter(IAuthorRepository authorRepository, IHttpContextAccessor accessor)
        {
            _authorRepository = authorRepository;
            _accessor = accessor;
        }

        public async Task<Author> GetRequester()
        {
            try
            {
                var userId = _accessor.GetUserId();

                // TODO - should not get the articles read except in a few cases
                return await _authorRepository.GetAuthor(userId).SingleAsync();
            }
            catch
            {
                return Author.NonLoggedInAuthor;
            }
        }

        public async Task<Author> GetRequesterWithArticles()
        {
            try
            {
                var userId = _accessor.GetUserId();

                // TODO - should not get the articles read except in a few cases
                return await _authorRepository.GetAuthorWithReadArticles(userId).SingleAsync();
            }
            catch
            {
                return Author.NonLoggedInAuthor;
            }
        }
    }
}
