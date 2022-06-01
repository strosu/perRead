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
        Task<FESectionWithArticles> UpdateSection(string sectionId, FESectionPreview sectionPreview);
        Task<IEnumerable<FESectionPreview>> ListSections();
        Task<FESectionPreview> GetSectionDetails(string sectionId);
        Task DeleteSection(string sectionId);
    }

    public class SectionsService : ISectionsService
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IRequesterGetter _requesterGetter;
        private readonly IFeedRepository _feedRepository;

        public SectionsService(ISectionRepository sectionRepository, IRequesterGetter requesterGetter, IFeedRepository feedRepository)
        {
            _sectionRepository = sectionRepository;
            _requesterGetter = requesterGetter;
            _feedRepository = feedRepository;
        }

        public async Task<FESectionWithArticles> CreateSection(SectionCommand sectionCommand)
        {
            var requester = await _requesterGetter.GetRequester();

            var section = await _sectionRepository.CreateNewSection(requester.AuthorId, sectionCommand);

            var feeds = await _feedRepository.GetUserFeeds(requester).ToListAsync();

            return section.ToFESectionWithArticles(requester, feeds);
        }

        public async Task DeleteSection(string sectionId)
        {
            var section = await _sectionRepository.GetSection(sectionId);

            if (section == null)
            {
                throw new ArgumentException("Section does not exist lol");
            }

            var requester = await _requesterGetter.GetRequester();


            if (section.AuthorId != requester.AuthorId)
            {
                throw new ArgumentException("You're not the owner here");
            }

            await _sectionRepository.DeleteSection(section);
        }

        public async Task<FESectionWithArticles> GetSectionArticles(string sectionId)
        {
            var section = await _sectionRepository.GetSection(sectionId);

            if (section == null)
            {
                throw new ArgumentException("Could not identify the session");
            }

            var requester = await _requesterGetter.GetRequesterWithArticles();

            var feeds = await _feedRepository.GetUserFeeds(requester).ToListAsync();

            return await _sectionRepository.GetSectionArticles(section.SectionId).Select(x => x.ToFESectionWithArticles(requester, feeds)).FirstOrDefaultAsync();
        }

        public async Task<FESectionPreview> GetSectionDetails(string sectionId)
        {
            var section = await _sectionRepository.GetSection(sectionId);
            return section.ToFESectionPreview();
        }

        public async Task<IEnumerable<FESectionPreview>> ListSections()
        {
            var requester = await _requesterGetter.GetRequester();
            return await _sectionRepository.GetAllSections()
                .Where(x => x.AuthorId == requester.AuthorId)
                .Select(x => x.ToFESectionPreview())
                .ToListAsync();
        }

        public async Task<FESectionWithArticles> UpdateSection(string sectionId, FESectionPreview sectionPreview)
        {
            var requester = await _requesterGetter.GetRequester();

            var section = await _sectionRepository.GetSection(sectionId);

            if (section.AuthorId != requester.AuthorId)
            {
                throw new ArgumentException("you don't own this");
            }

            var feeds = await _feedRepository.GetUserFeeds(requester).ToListAsync();

            return (await _sectionRepository.UpdateSection(section, new SectionCommand
            {
                Name = sectionPreview.Name,
                Description = sectionPreview.Description
            })).ToFESectionWithArticles(requester, feeds);
        }
    }
}

