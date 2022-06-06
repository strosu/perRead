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
        public async Task<FEFeedWithArticles> GetFeedArticles(string feedId)
        {
            // The feed is composed of a list of articles, based on the authors added to it
            // TODO - multiple feeds per user, need to take an argument here
            return await _feedsService.GetFeedArticles(feedId);
        }

        [HttpPost("/feeds/{feedId}/add/{sectionId}")]
        public async Task<IActionResult> AddSectionToFeed(string feedId, string sectionId)
        {
            await _feedsService.AddSectionToFeed(feedId, sectionId);
            return Ok();
        }

        [HttpPost("/feeds/addSection/{sectionId}")]
        public async Task<IActionResult> AddSectionToFeeds(string sectionId, [FromBody] IEnumerable<string> subscribedFeedIds)
        {
            await _feedsService.AddSectionToFeeds(sectionId, subscribedFeedIds);
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

        [HttpPost("/feeds/{feedId}/details")]
        public async Task UpdateFeedInfo(string feedId, FEFeedDetails feedDetails)
        {
            await _feedsService.UpdateFeedInfo(feedId, feedDetails);
        }

        [HttpDelete("/feeds/{feedId}")]
        public async Task DeleteFeed(string feedId)
        {
            await _feedsService.DeleteFeed(feedId);
        }
    }
}
