using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface ISectionsService
    {
        Task<FESectionWithArticles> GetSectionArticles(string sectionId);
        Task<FESectionWithArticles> CreateSection(SectionCommand sectionCommand);
        Task<FESectionWithArticles> UpdateSection(SectionCommand sectionCommand);
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

        public async Task<FESectionWithArticles> CreateSection(SectionCommand sectionCommand)
        {
            var requester = await _requesterGetter.GetRequester();

            var section = await _sectionRepository.CreateNewSection(requester.AuthorId, sectionCommand);

            return section.ToFESectionWithArticles(requester);
        }

        public async Task<FESectionWithArticles> GetSectionArticles(string sectionId)
        {
            var section = await _sectionRepository.GetSection(sectionId);

            if (section == null)
            {
                throw new ArgumentException("Could not identify the session");
            }

            var requester = await _requesterGetter.GetRequester();

            return await _sectionRepository.GetSectionArticles(section.SectionId).Select(x => x.ToFESectionWithArticles(requester)).FirstOrDefaultAsync();
        }

        public async Task<FESectionWithArticles> UpdateSection(SectionCommand sectionCommand)
        {
            var requester = await _requesterGetter.GetRequester();

            var section = await _sectionRepository.GetSection(sectionCommand.SectionId);

            if (section.AuthorId != requester.AuthorId)
            {
                throw new ArgumentException("you don't own this");
            }

            return (await _sectionRepository.UpdateSection(sectionCommand)).ToFESectionWithArticles(requester);
        }
    }
}

