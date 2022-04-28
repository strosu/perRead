﻿using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Extensions;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IRequesterGetter
    {
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
            var userId = _accessor.GetUserId();

            if (userId == null)
            {
                return Author.NonLoggedInAuthor;
            }

            return await _authorRepository.GetAuthorWithReadArticles(userId).SingleAsync();
        }
    }
}