using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IUserPreferenceService
    {
        Task<FEUserPreview?> GetUserPreview(string userId);
    }

    public class UserPreferenceService : IUserPreferenceService
    {
        private readonly IUserPreferenceRepository _userPreferenceRepository;

        public UserPreferenceService(IUserPreferenceRepository userPreferenceRepository)
        {
            _userPreferenceRepository = userPreferenceRepository;
        }

        public async Task<FEUserPreview?> GetUserPreview(string userId)
        {
            return 
                await _userPreferenceRepository.GetPreview(userId)
                .Select(x => x.ToUserPreview()).SingleOrDefaultAsync();
        }
    }
}
