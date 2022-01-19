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

        public async Task<ArticleModel> Create(string author, ArticleCommand article)
        {
            // Make sure the tags exist
            var tagMap = new List<TagModel>();

            foreach (var tag in article.Tags)
            {
                var foundTag = (await _context.Tags.FirstOrDefaultAsync(x => x.TagName == tag));

                if (foundTag != null)
                {
                    tagMap.Add(foundTag);
                }
                else
                {
                    var newTag = new TagModel { TagName = tag };
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
            var newArticle = new ArticleModel
            {
                Title = article.Title,
                Price = article.Price,
                Content = article.Content,
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

        public async Task Delete(ArticleModel article)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }

        public async Task<ArticleModel> Get(int id)
        {
            return await _context.Articles
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ArticleId == id);
        }

        public IQueryable<ArticleModel> GetAll()
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
    Task<ArticleModel> Create(string author, ArticleCommand article);

    IQueryable<ArticleModel> GetAll();

    Task<ArticleModel> Get(int id);

    Task Delete(ArticleModel article);
}
