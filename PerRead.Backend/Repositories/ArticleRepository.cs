using Microsoft.EntityFrameworkCore;
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

        public async Task<Article> Create(Article article)
        {
            throw new NotImplementedException();
        }

        public async Task<Article?> Get(int id)
        {
            return await _context.Articles.SingleOrDefaultAsync(x => x.ArticleId == id);
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            var articles = _context.Articles;
            if (await articles.AnyAsync())
            {
                return await articles.ToListAsync();
            }

            var author = new Author 
            { 
                Name = "Author1",
                AuthorId = 1,
                PopularityRank = 1
            };

            var tag = new Tag {TagId = 1, TagName = "politics" };

            _context.Tags.Add(tag);

            _context.Articles.Add(
                new Article 
                { 
                    ArticleId = 1,
                    Author = author,
                    Price = 11,
                    Title = "First Article",
                    Tags = new List<ArticleTag>() { new ArticleTag { TagId = 1, ArticleId = 1 } }
                });

            await _context.SaveChangesAsync();

            return _context.Articles;
        }
    }

    public interface IArticleRepository
    {
        Task<Article?> Create(Article article);

        Task<IEnumerable<Article>> GetAll();

        Task<Article?> Get(int id);
    }
}
