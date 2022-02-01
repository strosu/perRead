﻿using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IUserService
    {
        Task<FEUserPreview?> GetCurrentUserPreview();
        Task<long> AddMoreTokens();
    }

    public class UserService : IUserService
    {
        private readonly IUserPreferenceRepository _userPreferenceRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IHttpContextAccessor _accessor;

        public UserService(IUserPreferenceRepository userPreferenceRepository, IAuthorRepository authorRepository, IHttpContextAccessor accessor)
        {
            _userPreferenceRepository = userPreferenceRepository;
            _authorRepository = authorRepository;
            _accessor = accessor;
        }

        public async Task<long> AddMoreTokens()
        {
            var userId = _accessor.GetUserId();
            return await _authorRepository.AddMoreTokensAsync(userId);
        }

        public async Task<FEUserPreview?> GetCurrentUserPreview()
        {
            var authorId = _accessor.GetUserId();
            return await _authorRepository.GetAuthor(authorId).Select(x => x.ToUserPreview()).SingleAsync();
        }
    }
}
