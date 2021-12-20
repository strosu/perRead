using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models;
using PerRead.Backend.Models.Commands;

namespace PerRead.Backend.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Article> Create(ArticleCommand article)
        {
            // Make sure the tags exist
            var tagMap = new List<Tag>();

            //using (var transaction = _context.Database.BeginTransaction())
            //{

            //}

            foreach (var tag in article.Tags)
            {
                var foundTag = (await _context.Tags.FirstOrDefaultAsync(x => x.TagName == tag));

                if (foundTag != null)
                {
                    tagMap.Add(foundTag);
                }
                else
                {
                    var newTag = new Tag { TagName = tag };
                    _context.Tags.Add(newTag);
                    tagMap.Add(newTag);
                }
            }

            _context.SaveChanges();

            // Add the article
            var newArticle = new Article
            {
                Title = article.Title,
                Author = await _context.Authors.FirstAsync(),
                Price = article.Price,
            };

            _context.Articles.Add(newArticle);
            _context.SaveChanges();

            //transaction.Commit();

            newArticle.Tags = tagMap.Select(x => new ArticleTag
            {
                ArticleId = newArticle.ArticleId,
                TagId = x.TagId
            }).ToList();

            _context.Articles.Update(newArticle);

            //_context.SaveChanges();

            //transaction.Commit();

            return newArticle;
        }

        public async Task<Article?> Get(int id)
        {
            return await _context.Articles.SingleOrDefaultAsync(x => x.ArticleId == id);
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            var articles = _context.Articles;
            return await articles
                .Include(x => x.Author)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .ToListAsync();

            //if (await articles.AnyAsync())
            //{

            //}

            //var author = new Author 
            //{ 
            //    Name = "Author1",
            //    AuthorId = 1,
            //    PopularityRank = 1
            //};

            //var tag = new Tag {TagId = 1, TagName = "politics" };

            //_context.Tags.Add(tag);

            //_context.Articles.Add(
            //    new Article 
            //    { 
            //        ArticleId = 1,
            //        Author = author,
            //        Price = 11,
            //        Title = "First Article",
            //        Tags = new List<ArticleTag>() { new ArticleTag { TagId = 1, ArticleId = 1 } }
            //    });

            //await _context.SaveChangesAsync();

            //return _context.Articles;
        }
    }

    public interface IArticleRepository
    {
        Task<Article?> Create(ArticleCommand article);

        Task<IEnumerable<Article>> GetAll();

        Task<Article?> Get(int id);
    }
}
