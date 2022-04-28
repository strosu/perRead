using System;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface ISectionsService
    {
        Task<FESectionWithArticles> GetSectionsArticles(string authorId, string sectionName);
    }

    public class SectionsService : ISectionsService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ISectionRepository _sectionRepository;

        public SectionsService(IAuthorRepository authorRepository, ISectionRepository sectionRepository)
        {
            _authorRepository = authorRepository;
            _sectionRepository = sectionRepository;
        }

        public Task<FESectionWithArticles> GetSectionsArticles(string authorId, string sectionName)
        {
            var author = _authorRepository.GetAuthor(authorId);
        }
    }
}

