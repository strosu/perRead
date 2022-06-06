using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Services;

namespace PerRead.Backend.Controllers
{
    [ApiController]
    [Authorize]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestsService _requestsService;

        public RequestsController(IRequestsService requestsService)
        {
            _requestsService = requestsService;
        }

        [HttpGet("author/{authorId}/requests")]
        [AllowAnonymous]
        public async Task<IEnumerable<FERequestPreview>> GetRequestsForAuthor(string authorId)
        {
            return await _requestsService.GetRequestsForAuthor(authorId);
        }

        [HttpGet("requests/{requestId}")]
        [AllowAnonymous]
        public async Task<FERequest> GetRequest(string requestId)
        {
            return await _requestsService.GetRequest(requestId);
        }

        [HttpPost("requests")]
        public async Task<FERequest> CreateRequest([FromBody] CreateRequestCommand createRequestCommand)
        {
            return await _requestsService.CreateRequest(createRequestCommand);
        }

    }
}

