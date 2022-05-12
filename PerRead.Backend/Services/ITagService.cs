using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IRequesterGetter _requesterGetter;

        public TagService(ITagRepository tagRepository, IRequesterGetter requesterGetter)
        {
            _tagRepository = tagRepository;
            _requesterGetter = requesterGetter;
        }

        public async Task<FETag> GetTag(int id)
        {
            var requester = await _requesterGetter.GetRequesterWithArticles();

            return await _tagRepository.GetTabByTagId(id)
                .Select(tag => tag.ToFETag(requester)).FirstOrDefaultAsync();
        }

        public async Task<FETag> GetTag(string tag)
        {
            var requester = await _requesterGetter.GetRequesterWithArticles();

            return await _tagRepository.GetTagByName(tag)
                .Select(tag => tag.ToFETag(requester)).FirstOrDefaultAsync();
        }
    }

    public interface ITagService
    {
        Task<FETag> GetTag(string tag);

        Task<FETag> GetTag(int id);
    }
}
