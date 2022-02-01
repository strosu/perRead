using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Services;

namespace PerRead.Backend.Controllers
{
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/user/preview")]
        public async Task<IActionResult> GetCurrentUserPreview()
        {
            var result = await _userService.GetCurrentUserPreview();

            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}
