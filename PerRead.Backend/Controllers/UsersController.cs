using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Models.FrontEnd;
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

        [HttpPost("/user/addTokens/{amount}")]
        // TODO - rename this
        // TODO - need an actual implementation later
        public async Task<IActionResult> AddMoreTokens(int amount)
        {
            try
            {
                var currentTokenCount = await _userService.AddMoreTokens(amount);
                return Ok(currentTokenCount);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("/user/settings")]
        public async Task<FEUserSettings?> GetUserSettings()
        {
            return await _userService.GetCurrentUserSettings();
        }

        [HttpPost("/user/settings")]
        public async Task<IActionResult> UpdateUserSettings([FromBody] FEUserSettings settings)
        {
            try
            {
                await _userService.UpdateUserSettings(settings);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/user/acquired")]
        public async Task<IEnumerable<FEArticleUnlockInfo>> GetUnlockedArticles()
        {
            return await _userService.GetUnlockedArticles();
        }

        [HttpPost("/user/acquired")]
        public async Task UpdateUnlockedArticles([FromBody] IEnumerable<long> articleInfos)
        {
            await _userService.UpdateUnlockedArticles(articleInfos);
        }
    }
}
