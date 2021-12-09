using PerRead.Models;
using PerRead.Repositories;

namespace PerRead.Services
{
    /// <summary>
    /// Should contain all business logic
    /// </summary>
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public ArticleModel Create(ArticleModel articleModel)
        {
            return _articleRepository.Create(articleModel);
        }

        public IEnumerable<ArticleModel> GetAll()
        {
            return _articleRepository.GetAll();
        }

        public ArticleModel GetById(int id)
        {
            return _articleRepository.Get(id);
        }
    }

    public interface IArticleService
    {
        IEnumerable<ArticleModel> GetAll();

        ArticleModel GetById(int id);

        ArticleModel Create(ArticleModel articleModel);
    }
}
