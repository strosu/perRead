using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Filters;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;
using PerRead.Backend.Services;

namespace PerRead.Controllers
{
    [ApiController]
    [Authorize]
    //[Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("articles")]
        [ResponseHeader("Filter-Header", "Filter Value")]
        [AllowAnonymous]
        //[Route("")]
        public async Task<IEnumerable<FEArticlePreview>> GetAll()
        {
            return await _articleService.GetAll();
        }

        [HttpGet("articles/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var article = await _articleService.Get(id);

            if (article == null)
            {
                return NotFound();
            }

            //var paymentResult = await _paymentService.Settle(id, article.AuthorPreviews.First().AuthorId, article.Price);

            var paymentResult = await _articleService.UnlockForCurrentUser(id);

            if (paymentResult.Result == PaymentResultEnum.Success)
            {
                return Ok(article);
            }

            return BadRequest(paymentResult.Reason);
        }

        [HttpPost("articles")]
        public async Task<IActionResult> Post([FromBody] CreateArticleCommand articleCommand)
        {
            try
            {
                var article = await _articleService.Create(articleCommand);
                return Ok(article);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("articles/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _articleService.Delete(id);
                return NoContent();

            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        [HttpGet("articles/{id}/owners")]
        public async Task<IActionResult> GetOwners(string id)
        {
            var article = await _articleService.Get(id);

            if (article == null)
            {
                return NotFound();
            }

            try
            {
                return Ok(await _articleService.GetOwnership(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("articles/{id}/owners")]
        public async Task<IActionResult> UpdateOwners(string id, [FromBody] UpdateOwnershipCommand ownership)
        {
            return Ok(await _articleService.SetOwnership(id, ownership));
        }
    }
}