using PerRead.Backend.Models;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAll();

        Task<Article?> Get(int id);

        Task<Article?> Create(Article articleModel);
    }

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<Article?> Create(Article articleModel)
        {
            return await _articleRepository.Create(articleModel);
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _articleRepository.GetAll();
        }

        public async Task<Article?> Get(int id)
        {
            return await _articleRepository.Get(id);

        }
    }
}

