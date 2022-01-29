using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Services;

namespace PerRead.Backend.Controllers
{
    [ApiController]
    [Authorize]
    public class UserPrerefencesController : ControllerBase
    {
        private readonly IUserPreferenceService _userService;

        public UserPrerefencesController(IUserPreferenceService userService)
        {
            _userService = userService;
        }

        [HttpGet("/user/preview")]
        public async Task<IActionResult> GetCurrentUserPreview()
        {
            var user = HttpContext.User;

            var target = user?.Identity?.Name;

            if (target == null)
            {
                return Unauthorized();
            }

            var userModel = await _userService.GetUserPreview(target);

            if (userModel == null)
            {
                return Unauthorized();
            }

            return Ok(userModel);
        }
    }
}
