using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Services;

namespace PerRead.Backend.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class SectionsController
    {
        private readonly ISectionsService _sectionsService;

        public SectionsController(ISectionsService sectionsService)
        {
            _sectionsService = sectionsService;
        }

        // TODO - revisit if we should get using the author id (or name?) and section name
        // API wise it's cleaner to request for the ID, URL wise is nicer to see user-friendly strings
        [HttpGet("section/{sectionId}/articles")]
        public async Task<FESectionWithArticles> GetSectionArticles(string sectionId)
        {
            return await _sectionsService.GetSectionArticles(sectionId);
        }

        [HttpGet("section/{sectionId}/details")]
        public async Task<FESectionPreview> GetSectionDetails(string sectionId)
        {
            return await _sectionsService.GetSectionDetails(sectionId);
        }

        [HttpPost("sections/add")]
        public async Task<FESectionWithArticles> CreateSection([FromBody] SectionCommand sectionCommand)
        {
            return await _sectionsService.CreateSection(sectionCommand);
        }

        [HttpPost("sections/{sectionId}/edit")]
        public async Task<FESectionWithArticles> UpdateSection(string sectionId, [FromBody] FESectionPreview sectionPreview)
        {
            return await _sectionsService.UpdateSection(sectionId, sectionPreview);
        }

        [HttpGet("sections")]
        public async Task<IEnumerable<FESectionPreview>> ListSections()
        {
            return await _sectionsService.ListSections();
        }

        [HttpDelete("/sections/{sectionId}")]
        public async Task DeleteFeed(string sectionId)
        {
            await _sectionsService.DeleteSection(sectionId);
        }
    }
}

