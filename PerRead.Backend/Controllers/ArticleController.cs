using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Filters;
using PerRead.Backend.Models;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Services;

namespace PerRead.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("")]
        [ResponseHeader("Filter-Header", "Filter Value")]
        [AllowAnonymous]
        //[Route("")]
        public async Task<IEnumerable<FEArticlePreview>> GetAll()
        {
            return await _articleService.GetAll();
        }

        [HttpGet("{id}")]
        //[Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await _articleService.Get(id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        [HttpPost("")]
        //[Route("create")]
        public async Task<IActionResult> Post([FromBody] ArticleCommand articleCommand)
        {
            try
            {
                var user = HttpContext.User?.Identity?.Name;

                if (user == null)
                {
                    // This shold never be the case, Identity should protect the endpoint and get us the Identity object automatically
                    return Forbid();
                }

                var article = await _articleService.Create(user.ToString(), articleCommand);
                return Ok(article);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
    }
}