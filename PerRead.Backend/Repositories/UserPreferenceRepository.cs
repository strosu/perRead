using PerRead.Backend.Models.BackEnd;
using System.Linq;

namespace PerRead.Backend.Repositories
{
    public class UserPreferenceRepository : IUserPreferenceRepository
    {
        public IQueryable<UserPreferences> GetPreview(string userId)
        {
            return null;
        }
    }

    public interface IUserPreferenceRepository
    {
        IQueryable<UserPreferences> GetPreview(string userId);
    }
}
