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

            await _context.SaveChangesAsync();


            // Get the authors
            var authors = await _context.Authors.Where(x => article.Authors.Contains(x.Name)).ToListAsync();

            // Add the article
            var newArticle = new Article
            {
                Title = article.Title,
                Price = article.Price,
                Content = article.Content,
            };

            newArticle.ArticleAuthors = authors.Select(x => new ArticleAuthor
            {
                Article = newArticle,
                Author = x,
                Order = 1
            }).ToList();

            _context.Articles.Add(newArticle);
            await _context.SaveChangesAsync();

            //transaction.Commit();

            //var createdArticle = await _context.Articles.FindAsync(newArticle.ArticleId);
            //_context.Articles.Update(createdArticle);

            newArticle.Tags = tagMap.ToList();

            // Edit for simplicity - TODO, remove this at some point
            newArticle.Title += newArticle.ArticleId;
            newArticle.Content += newArticle.ArticleId;

            await _context.SaveChangesAsync();

            //transaction.Commit();

            return newArticle;
        }

        public async Task Delete(Article article)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }

        public async Task<Article?> Get(int id)
        {
            return await _context.Articles
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ArticleId == id);
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            await EnsureSeeded();

            var articles = _context.Articles;
            return await articles
                .AsNoTracking()
                .Include(x => x.ArticleAuthors)
                    .ThenInclude(al => al.Author)
                .Include(x => x.Tags)
                //.ThenInclude(x => x.Tag)
                .ToListAsync();
        }

        private async Task EnsureSeeded()
        {
            if (await _context.Articles.AnyAsync())
            {
                return;
            }

            var author = new Author
            {
                Name = "gogu1",
                AuthorId = 1,
                PopularityRank = 1
            };

            _context.Authors.Add(author);

            var article = new Article
            {
                ArticleId = 1,
                Content = "seeded content",
                Price = 1,
                Title = "seeded title"
            };

            article.ArticleAuthors = new List<ArticleAuthor>() { new ArticleAuthor
            {
                Article = article,
                Author = author,
                Order = 1
            } };

            _context.Articles.Add(article);


            await _context.SaveChangesAsync();
        }
    }

    public interface IArticleRepository
    {
        Task<Article?> Create(ArticleCommand article);

        Task<IEnumerable<Article>> GetAll();

        Task<Article?> Get(int id);

        Task Delete(Article article);
    }
}
