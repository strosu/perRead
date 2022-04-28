using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Repositories
{
    public interface ISectionRepository
    {
        Task<Section> GetSection(string sectionId);

        IQueryable<Section> GetSectionArticles(string sectionId);
    }

    public class SectionRepository : ISectionRepository
    {
        private readonly AppDbContext _context;

        public SectionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Section> GetSection(string sectionId)
        {
            return await _context.Sections.Where(x => x.SectionId == sectionId).SingleOrDefaultAsync();
        }

        public IQueryable<Section> GetSectionArticles(string sectionId)
        {
            return _context.Sections.Where(x => x.SectionId == sectionId)
                .Include(x => x.Articles)
                .ThenInclude(x => x.Article);
        }
    }
}

