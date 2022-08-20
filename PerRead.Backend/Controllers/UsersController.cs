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
        private readonly IWalletService _walletService;

        public UsersController(IUserService userService, IWalletService walletService)
        {
            _userService = userService;
            _walletService = walletService;
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

        [HttpPost("/user/tokens/add/{amount}")]
        // TODO - rename this
        // TODO - need an actual implementation later
        public async Task<IActionResult> AddMoreTokens(int amount)
        {
            try
            {
                var currentTokenCount = await _walletService.AddTokensForCurrentUser(amount);
                return Ok(currentTokenCount);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("user/tokens/withdraw/{amount}")]
        public async Task<IActionResult> WithdrawTokens(int amount)
        {
            try
            {
                var currentTokenCount = await _walletService.WithdrawTokensForCurrentUser(amount);
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
        public async Task UpdateUnlockedArticles([FromBody] IEnumerable<string> articleInfos)
        {
            await _userService.UpdateUnlockedArticles(articleInfos);
        }

        [HttpGet("/user/wallet/{walletId}")]
        public async Task<FEWallet> GetWallet(string walletId)
        {
            return await _walletService.GetWallet(walletId);
        }

        [HttpGet("/user/wallet")]
        public async Task<FEWallet> GetMainWallet()
        {
            return await _walletService.GetCurrentUserMainWallet();
        }

        [HttpGet("/user/escrow")]
        public async Task<FEWallet> GetEscrowWallet()
        {
            return await _walletService.GetCurrentUserEscrowWallet();
        }
    }
}
