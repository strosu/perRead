using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Repositories;
using System.Security.Claims;

namespace PerRead.Backend.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this IHttpContextAccessor accessor)
        {
            return accessor.GetClaimValue(ClaimTypes.NameIdentifier);
        }

        public static string GetUserName(this IHttpContextAccessor accessor)
        {
            return accessor.GetClaimValue(ClaimTypes.Name);
        }

        public static string GetClaimValue(this IHttpContextAccessor accessor, string key)
        {
            var claim = accessor?.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == key);

            if (claim == null)
            {
                throw new InvalidOperationException("Could not identify the user, please login");
            }

            return claim.Value;
        }
    }
}
