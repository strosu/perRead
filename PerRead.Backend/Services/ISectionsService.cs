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
        Task<FESectionWithArticles> UpdateSection(string sectionId, SectionCommand sectionCommand);
        Task<IEnumerable<FESectionPreview>> ListSections();
    }

    public class SectionsService : ISectionsService
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IRequesterGetter _requesterGetter;

        public SectionsService(ISectionRepository sectionRepository, IRequesterGetter requesterGetter)
        {
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

        public async Task<IEnumerable<FESectionPreview>> ListSections()
        {
            var requester = await _requesterGetter.GetRequester();
            return await _sectionRepository.GetAllSections()
                .Where(x => x.AuthorId == requester.AuthorId)
                .Select(x => x.ToFESectionPreview())
                .ToListAsync();
        }

        public async Task<FESectionWithArticles> UpdateSection(string sectionId, SectionCommand sectionCommand)
        {
            var requester = await _requesterGetter.GetRequester();

            var section = await _sectionRepository.GetSection(sectionId);

            if (section.AuthorId != requester.AuthorId)
            {
                throw new ArgumentException("you don't own this");
            }

            return (await _sectionRepository.UpdateSection(section, sectionCommand)).ToFESectionWithArticles(requester);
        }
    }
}

