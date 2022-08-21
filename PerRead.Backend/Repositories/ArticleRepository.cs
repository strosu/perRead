using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Helpers.Errors;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.Useful;
using PerRead.Backend.Repositories.Extensions;

namespace PerRead.Backend.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Article> Create(Author author, IEnumerable<Tag> tags, IEnumerable<Section> sections, string articleImagePath, CreateArticleCommand article)
        {
            var now = DateTime.UtcNow;

            // Add the article
            var newArticle = new Article
            {
                ArticleId = Guid.NewGuid().ToString(),
                Title = article.Title,
                Price = article.Price,
                Content = article.Content,
                CreatedAt = now,
                ImageUrl = articleImagePath,
            };

            newArticle.Sections = sections.Select(section => new SectionArticle
            {
                Article = newArticle,
                SectionId = section.SectionId
            }).ToList();

            newArticle.AuthorsLink = new List<ArticleOwner>
            {
                new ArticleOwner
                {
                    Article = newArticle,
                    Author = author,
                    CanBeEdited = true, // The initial author cannot be removed
                    IsUserFacing = true,
                    OwningPercentage = 1, // Initially the main author owns the article in its entirety
                    IsPublisher = true
                }
            };

            newArticle.Tags = tags.ToList();

            _context.Articles.Add(newArticle);
            //_context.Entry(author).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();

            return newArticle;
        }

        public async Task Delete(string id)
        {
            var article = await _context.Articles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ArticleId == id);

            if (article == null)
            {
                // No article found, nothing to delete
                return;
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Article> GetInternal(string id, bool withTracking = false)
        {
            return _context.Articles
                .WithTrackingIfNeeded(withTracking)
                .Where(x => x.ArticleId == id)
                .IncludeOwners()
                .IncludeTags()
                .IncludeSections();
        }

        public IQueryable<Article> GetVisible(string id, string requesterId, bool withTracking = false)
        {
            return GetInternal(id, withTracking).IfEligible(requesterId).IncludeReviews();
        }

        public IQueryable<Article> GetAllInternal(bool withTracking = false)
        {
            return _context.Articles
                .WithTrackingIfNeeded(withTracking)
                .IncludeOwners()
                .IncludeSections()
                .IncludeTags();
        }

        public IQueryable<Article> GetAllVisible(string requesterId, bool withTracking = false)
        {
            return GetAllInternal(withTracking).IfEligible(requesterId);
        }

        public IQueryable<Article> GetLatestArticles(string authorId)
        {
            return GetAllInternal().Where(x => x.AuthorsLink.Any(a => a.AuthorId == authorId && a.IsUserFacing))
                .OrderByDescending(x => x.CreatedAt).Take(20);
        }

        public IQueryable<Article> GetLatestVisibleArticles(string authorId, string requesterId)
        {
            return GetAllVisible(requesterId).Where(x => x.AuthorsLink.Any(a => a.AuthorId == authorId && a.IsUserFacing))
                .OrderByDescending(x => x.CreatedAt).Take(20);
        }

        public IQueryable<Article> GetWithOwnersInternal(string id, bool withTracking = false)
        {
            return _context.Articles
                .Where(x => x.ArticleId == id)
                .WithTrackingIfNeeded(withTracking)
                .IncludeOwners();
        }

        public IQueryable<Article> GetVisibleWithOwners(string id, string requesterId, bool withTracking = false)
        {
            return GetWithOwnersInternal(id, withTracking).IfEligible(requesterId);
        }

        public async Task MarkAsOwnersOnly(Article article)
        {
            article.VisibleOnlyToOwners = true;
            await _context.SaveChangesAsync();
        }

        public async Task<Article> UpdateOwners(Article article, IEnumerable<AuthorOwnership> owners)
        {
            var ownersToRemove = article.AuthorsLink.Where(x => !owners.Any(y => y.Author.AuthorId == x.AuthorId));

            foreach (var toRemove in ownersToRemove)
            {
                article.AuthorsLink.Remove(toRemove);
            }

            var newOwners = new List<ArticleOwner>();

            foreach (var owner in owners)
            {
                var previousValue = article.AuthorsLink.FirstOrDefault(x => x.AuthorId == owner.Author.AuthorId);
                if (previousValue == null)
                {
                    article.AuthorsLink.Add(new ArticleOwner
                    {
                        AuthorId = owner.Author.AuthorId,
                        ArticleId = article.ArticleId,
                        CanBeEdited = owner.CanBeEdited,
                        IsUserFacing = owner.IsUserFacing,
                        OwningPercentage = owner.Ownership
                    });
                }
                else
                {
                    previousValue.OwningPercentage = owner.Ownership;
                }
            }

            await _context.SaveChangesAsync();

            return await _context.Articles
                .Include(x => x.AuthorsLink).FirstAsync(x => x.ArticleId == article.ArticleId);
        }

        public async Task<Article> UpdateSourceRequest(Article article, ArticleRequest request)
        {
            //article.SourceRequest = request;
            await _context.SaveChangesAsync();

            return article;
        }

        public async Task<Article> SetReview(string articleId, string requesterId, bool? recommends)
        {
            var article = await GetVisible(articleId, requesterId, true)
                .IncludeReviews().SingleAsync();

            if (article == null)
            {
                throw new NotFoundException("Could not find the article");
            }

            var currentRequesterUnlock = article.Reviews.FirstOrDefault(x => x.AuthorId == requesterId);

            if (currentRequesterUnlock == null)
            {
                throw new NotFoundException("You havent unlocked this article");
            }

            var previousValue = currentRequesterUnlock.Recommends;
            currentRequesterUnlock.Recommends = recommends;

            if (previousValue.HasValue)
            {
                // If we had a value previously, make sure to clear it
                if (previousValue.Value)
                {
                    article.RecommendsReadingCount--;
                }
                else
                {
                    article.NotRecommendsReadingCount--;
                }

                await _context.SaveChangesAsync();
            }


            if (recommends == null)
            {
                // Nothing, we already cleared
                return article;
            }

            if (recommends.Value)
            {
                article.RecommendsReadingCount++;
            }
            else
            {
                article.NotRecommendsReadingCount++;
            }

            await _context.SaveChangesAsync();

            return article;
        }
    }
}

public interface IArticleRepository
{
    // TODO - extract parameters to something else
    Task<Article> Create(Author author, IEnumerable<Tag> tags, IEnumerable<Section> sections, string articleImagePath, CreateArticleCommand article);

    Task Delete(string id);

    Task MarkAsOwnersOnly(Article article);

    Task<Article> UpdateOwners(Article article, IEnumerable<AuthorOwnership> owners);

    Task<Article> UpdateSourceRequest(Article article, ArticleRequest request);

    IQueryable<Article> GetAllInternal(bool withTracking = false);
    
    IQueryable<Article> GetAllVisible(string requesterId, bool withTracking = false);

    IQueryable<Article> GetInternal(string id, bool withTracking = false);

    IQueryable<Article> GetVisible(string id, string requesterId, bool withTracking = false);

    IQueryable<Article> GetWithOwnersInternal(string id, bool withTracking = false);

    IQueryable<Article> GetVisibleWithOwners(string id, string requesterId, bool withTracking = false);

    IQueryable<Article> GetLatestVisibleArticles(string authorId, string requesterId);

    Task<Article> SetReview(string articleId, string authorId, bool? recommends);

}
