using System.Security.Claims;

namespace PerRead.Backend.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this IHttpContextAccessor accessor)
        {
            var claim = accessor?.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                throw new InvalidOperationException("Could not identify the user, please login");
            }

            return claim.Value;
        }
    }
}
