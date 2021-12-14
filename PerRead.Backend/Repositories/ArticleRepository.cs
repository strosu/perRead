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

        public async Task<IEnumerable<Article>> GetAll()
        {
            var articles = _context.Articles;
            if (articles.Any())
            {
                return articles;
            }

            var author = new Author 
            { 
                Name = "Author1",
                AuthorId = 1,
                PopularityRank = 1
            };

            _context.Articles.Add(
                new Article 
                { 
                    ArticleId = 1,
                    Author = author,
                    Price = 11,
                    Title = "First Article",
                    Tags = new List<Tag>() { new Tag { TagId = 1, TagName ="politics" } }
                });

            await _context.SaveChangesAsync();

            return _context.Articles;
        }
    }

    public interface IArticleRepository
    {
        Article Create(Article article);

        Task<IEnumerable<Article>> GetAll();

        Article Get(int id);
    }
}
