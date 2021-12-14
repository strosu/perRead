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
        public IEnumerable<Article> Get()
        {
            return _articleService.GetAll();
        }

    }
}