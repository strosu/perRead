using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<FETag> GetTag(int id)
        {
            return await _tagRepository.GetTabByTagId(id)
                .Select(tag => tag.ToFETag()).FirstOrDefaultAsync();
        }

        public async Task<FETag> GetTag(string tag)
        {
            return await _tagRepository.GetTagByName(tag)
                .Select(tag => tag.ToFETag()).FirstOrDefaultAsync();
        }
    }

    public interface ITagService
    {
        Task<FETag> GetTag(string tag);
        
        Task<FETag> GetTag(int id);
    }
}
