using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Services;

namespace PerRead.Backend.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class SectionController
    {
        private readonly ISectionsService _sectionsService;

        public SectionController(ISectionsService sectionsService)
        {
            _sectionsService = sectionsService;
        }

        [HttpGet("author/{authorId}/section/{sectionName}")]
        public async Task<FESectionWithArticles> GetSectionArticles(string authorId, string sectionName)
        {
            return await _sectionsService.GetSectionArticles(authorId, sectionName);
        }
    }
}

