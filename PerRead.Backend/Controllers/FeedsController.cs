using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Services;

namespace PerRead.Backend.Controllers
{
    [ApiController]
    [Authorize]
    public class FeedsController : ControllerBase
    {
        private readonly IFeedsService _feedsService;

        public FeedsController(IFeedsService feedsService)
        {
            _feedsService = feedsService;
        }

        [HttpGet("/feeds/{feedId}")]
        public async Task<FEFeedWithArticles> GetFeed(string feedId)
        {
            // The feed is composed of a list of articles, based on the authors added to it
            // TODO - multiple feeds per user, need to take an argument here
            return await _feedsService.GetFeed(feedId);
        }

        [HttpPost("/feeds/{feedId}/add/{authorId}")]
        public async Task<IActionResult> AddAuthorToFeed(string feedId, string authorId)
        {
            await _feedsService.AddAuthorToFeed(feedId, authorId);
            return Ok();
        }

        [HttpGet("/feeds")]
        public async Task<IEnumerable<FEFeedPreview>> GetFeeds()
        {
            return await _feedsService.GetFeeds();
        }

        [HttpPost("/feeds/add/{feedName}")]
        public async Task<FEFeedWithArticles> AddFeed(string feedName)
        {
            var feed = await _feedsService.CreateNewFeed(feedName);

            return feed;
        }

        [HttpGet("/feeds/{feedId}/details")]
        public async Task<FEFeedDetails> GetFeedInfo(string feedId)
        {
            return await _feedsService.GetFeedInfo(feedId);
        }
    }
}
