﻿using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Extensions;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IUserService
    {
        Task<FEUserPreview?> GetCurrentUserPreview();

        Task<FEUserSettings?> GetCurrentUserSettings();

        Task UpdateUserSettings(FEUserSettings userSettings);

        Task<IEnumerable<FEArticleUnlockInfo>> GetUnlockedArticles();

        Task UpdateUnlockedArticles(IEnumerable<string> articleInfos);
    }

    public class UserService : IUserService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IHttpContextAccessor _accessor;

        public UserService(IUserPreferenceRepository userPreferenceRepository, IAuthorRepository authorRepository, IHttpContextAccessor accessor)
        {
            _authorRepository = authorRepository;
            _accessor = accessor;
        }

        public async Task<FEUserPreview?> GetCurrentUserPreview()
        {
            var authorId = _accessor.GetUserId();
            return await _authorRepository.GetAuthor(authorId).Select(x => x.ToUserPreview()).SingleAsync();
        }

        public async Task<FEUserSettings?> GetCurrentUserSettings()
        {
            var authorId = _accessor.GetUserId();

            return await _authorRepository.GetAuthor(authorId).Select(x => x.ToFEUserSettings()).SingleAsync();
        }

        public async Task<IEnumerable<FEArticleUnlockInfo>> GetUnlockedArticles()
        {
            var authorId = _accessor.GetUserId();
            return await _authorRepository.GetAuthorWithReadArticles(authorId)
                .SelectMany(x => x.UnlockedArticles).Select(x => x.ToFEArticleUnlockInfo()).ToListAsync();
        }

        public async Task UpdateUnlockedArticles(IEnumerable<string> articleInfos)
        {
            var authorId = _accessor.GetUserId();
            await _authorRepository.UpdateReadArticles(authorId, articleInfos);
        }

        public async Task UpdateUserSettings(FEUserSettings userSettings)
        {
            var authorId = _accessor.GetUserId();
            await _authorRepository.UpdateSettings(authorId, userSettings);
        }
    }
}
