using System;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Services;

namespace PerRead.Backend.Controllers
{
    [ApiController]
    public class SectionController
    {
        private readonly ISectionsService _sectionsService;

        public SectionController(ISectionsService sectionsService)
        {
            _sectionsService = sectionsService;
        }

        [HttpGet("/{authorId}/{sectionId}")]
        public async Task<FESectionWithArticles> GetSectionArticles(string authorId, string sectionName)
        {
            return await _sectionsService.GetSectionArticles(authorId, sectionName);
        }
    }
}

