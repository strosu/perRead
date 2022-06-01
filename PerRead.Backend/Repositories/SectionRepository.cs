using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Commands;

namespace PerRead.Backend.Repositories
{
    public interface ISectionRepository
    {
        Task<Section> GetSection(string sectionId);

        IQueryable<Section> GetSectionArticles(string sectionId);

        IQueryable<Section> GetAllSections();

        Task<Section> CreateNewSection(string authorId, SectionCommand sectionCommand);

        Task<Section> UpdateSection(Section section, SectionCommand sectionCommand);
        Task DeleteSection(Section section);
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
                .ThenInclude(x => x.Article)
                .ThenInclude(x => x.ArticleAuthors)
                .ThenInclude(x => x.Author)
                .Include(x => x.Articles)
                .ThenInclude(x => x.Article)
                .ThenInclude(x => x.Tags);
        }

        public async Task<Section> CreateNewSection(string authorId, SectionCommand sectionCommand)
        {
            var newSection = new Section
            {
                AuthorId = authorId,
                Name = sectionCommand.Name,
                Description = sectionCommand.Description,
                SectionId = Guid.NewGuid().ToString(),
            };

            _context.Sections.Add(newSection);
            await _context.SaveChangesAsync();

            return newSection;
        }

        public async Task<Section> UpdateSection(Section section, SectionCommand sectionCommand)
        {
            section.Name = sectionCommand.Name;
            section.Description = sectionCommand.Description;

            await _context.SaveChangesAsync();

            return section;
        }

        public IQueryable<Section> GetAllSections()
        {
            return _context.Sections.AsNoTracking();
        }

        public async Task DeleteSection(Section section)
        {
            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
        }
    }
}

