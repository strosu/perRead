using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Services;
using System.Security.Claims;

namespace PerRead.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException();
            }

            var author = await _authorsService.GetAuthorAsync(id);

            if (author != null)
            {
                return Ok(author);
            }

            return NotFound();
        }

        [HttpGet("")]
        public async Task<IEnumerable<FEAuthorPreview>> GetAll()
        {
            return await _authorsService.GetAuthorsAsync();
        }
    }
}

