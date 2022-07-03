using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Services;

namespace PerRead.Backend.Controllers
{
    [ApiController]
    [Authorize]
    public class PledgeController : ControllerBase
    {
        private readonly IPledgeService _pledgeService;

        public PledgeController(IPledgeService pledgeService)
        {
            _pledgeService = pledgeService;
        }

        [HttpGet("pledges/{pledgeid}")]
        public async Task<FEPledge> GetPledge(string pledgeId)
        {
            return await _pledgeService.GetPledge(pledgeId);
        }

        [HttpPost("pledges/add")]
        public async Task<FEPledge> AddPledge([FromBody] PledgeCommand pledgeCommand)
        {
            return await _pledgeService.AddPledge(pledgeCommand);
        }

        [HttpPost("pledges/edit")]
        public async Task<FEPledge> EditPledge([FromBody] PledgeCommand pledgeComand)
        {
            return await _pledgeService.EditPledge(pledgeComand);
        }

        [HttpDelete("pledges/{pledgeId}")]
        public async Task<FERequest> DeletePledge(string pledgeId)
        {
            return await _pledgeService.RemovePledge(pledgeId);
        }
    }
}

