using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models;
using PerRead.Backend.Models.BackEnd;
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

        public async Task<Article> Create(string author, ArticleCommand article)
        {
            var now = DateTime.Now;

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
                    var newTag = new Tag { TagName = tag, FirstUsage = now };
                    _context.Tags.Add(newTag);
                    tagMap.Add(newTag);
                }
            }

            await _context.SaveChangesAsync();


            // Get the authors
            //var authors = await _context.Authors.Where(x => article.Authors.Contains(x.Name)).ToListAsync();

            var dbAuthor = await _context.Authors.FirstOrDefaultAsync(x => x.Name == author);

            if (dbAuthor == null)
            {
                throw new ArgumentNullException("Author not found lol");
            }

            // Add the article
            var newArticle = new Article
            {
                Title = article.Title,
                Price = article.Price,
                Content = article.Content,
                CreatedAt = now,
            };

            newArticle.ArticleAuthors = new List<ArticleAuthor>
            {
                new ArticleAuthor
            {
                Article = newArticle,
                Author = dbAuthor,
                Order = 1
            }
            };

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
                .Where(x => x.ArticleId == id)
                .Include(article => article.ArticleAuthors)
                    .ThenInclude(articleAuthor => articleAuthor.Author)
                .Include(a => a.Tags)
                .SingleOrDefaultAsync();
        }

        public IQueryable<Article> GetAll()
        {
            return _context.Articles
                .AsNoTracking()
                .Include(x => x.ArticleAuthors)
                    .ThenInclude(al => al.Author)
                .Include(x => x.Tags);
        }
    }
}

public interface IArticleRepository
{
    Task<Article> Create(string author, ArticleCommand article);

    IQueryable<Article> GetAll();

    Task<Article?> Get(int id);

    Task Delete(Article article);
}
