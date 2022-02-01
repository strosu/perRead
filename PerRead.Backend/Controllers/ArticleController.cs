using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Filters;
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
        private readonly IPaymentService _paymentService;

        public ArticleController(IArticleService articleService, IPaymentService paymentService)
        {
            _articleService = articleService;
            _paymentService = paymentService;
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

            var paymentResult = await _paymentService.Settle(article.AuthorPreviews.First().AuthorId, article.Price);

            if (paymentResult.Result == PaymentResultEnum.Success)
            {
                return Ok(article);
            }

            return BadRequest(paymentResult.Reason);
        }

        [HttpPost("")]
        //[Route("create")]
        public async Task<IActionResult> Post([FromBody] ArticleCommand articleCommand)
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