using PerRead.Backend.Models;

namespace PerRead.Backend.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public Article Create(Article article)
        {
            throw new NotImplementedException();
        }

        public Article Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetAll()
        {
            return _context.Articles;
        }
    }

    public interface IArticleRepository
    {
        Article Create(Article article);

        IEnumerable<Article> GetAll();

        Article Get(int id);
    }
}
