using PerRead.Models;

namespace PerRead.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public ArticleModel Create(ArticleModel article)
        {
            throw new NotImplementedException();
        }

        public ArticleModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }

    public interface IArticleRepository
    {
        ArticleModel Create(ArticleModel article);

        IEnumerable<ArticleModel> GetAll();

        ArticleModel Get(int id);
    }
}
