using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface ISectionsService
    {
        Task<FESectionWithArticles> GetSectionArticles(string authorId, string sectionName);
    }

    public class SectionsService : ISectionsService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IRequesterGetter _requesterGetter;

        public SectionsService(IAuthorRepository authorRepository, ISectionRepository sectionRepository, IRequesterGetter requesterGetter)
        {
            _authorRepository = authorRepository;
            _sectionRepository = sectionRepository;
            _requesterGetter = requesterGetter;
        }

        public async Task<FESectionWithArticles> GetSectionArticles(string authorId, string sectionName)
        {
            var section = await _authorRepository.GetAuthorSections(authorId)
                .Where(x => x.Name == sectionName).FirstOrDefaultAsync();

            if (section == null)
            {
                throw new ArgumentException("Could not identify the session");
            }

            var requester = await _requesterGetter.GetRequester();

            return await _sectionRepository.GetSectionArticles(section.SectionId).Select(x => x.ToFESectionWithArticles(requester)).FirstOrDefaultAsync();
        }
    }
}

