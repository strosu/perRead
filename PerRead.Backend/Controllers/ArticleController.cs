using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Models;
using PerRead.Backend.Services;

namespace PerRead.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Article>> Get()
        {
            return await _articleService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await _articleService.Get(id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

    }
}