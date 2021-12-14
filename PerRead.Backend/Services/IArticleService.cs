using PerRead.Backend.Models;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetAll();

        Article GetById(int id);

        Article Create(Article articleModel);
    }

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public Article Create(Article articleModel)
        {
            return _articleRepository.Create(articleModel);
        }

        public IEnumerable<Article> GetAll()
        {
            return _articleRepository.GetAll();
        }

        public Article GetById(int id)
        {
            return _articleRepository.Get(id);

        }
        //public IEnumerable<Article> GetAll()
        //{
        //    return new List<Article> 
        //    {
        //        new Article() { Title = "First", Id = 1, Price = 11, Tags = new List<string> { "politics" } },
        //        new Article() { Title = "Second", Id = 2, Price = 22, Tags = new List<string> { "entertainment" } }
        //    };
        //}
    }
}

