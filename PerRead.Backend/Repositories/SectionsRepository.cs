using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Repositories
{
    public interface ISectionRepository
    {
        Task<Section> GetSection(string sectionId);
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
    }
}

