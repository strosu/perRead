﻿using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Helpers.Errors;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Author> CreateAsync(AuthorCommand command)
        {
            var newAuthor = new Author
            {
                AuthorId = command.Id,
                Name = command.Name,
                MainWallet = command.MainWallet,
                EscrowWallet = command.EscrowWallet,
            };

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();

            return newAuthor;
        }

        public IQueryable<Author> GetAuthor(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _context.Authors
                .Where(author => author.AuthorId == id)
                .Include(author => author.MainWallet)
                .Include(author => author.EscrowWallet);
        }

        public IQueryable<Section> GetAuthorSections(string authorId)
        {
            return GetAuthor(authorId)
                .Include(author => author.PublishSections)
                .SelectMany(x => x.PublishSections);
        }

        public IQueryable<Author> GetAuthorWithArticles(string id)
        {
            return GetAuthor(id)
                .Include(author => author.PublishSections)
                    .ThenInclude(author => author.Articles)
                    .ThenInclude(articleAuthor => articleAuthor.Article)
                    .ThenInclude(article => article.Tags);
        }

        public IQueryable<Author> GetAuthorWithReadArticles(string id)
        {
            return GetAuthor(id)
                .Include(author => author.UnlockedArticles)
                .ThenInclude(articleUnlock => articleUnlock.Article)
                .ThenInclude(article => article.AuthorsLink)
                .ThenInclude(aa => aa.Author);
        }

        public IQueryable<Author> GetAuthorByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return _context.Authors.AsNoTracking()
                .Where(x => x.Name == name);
        }

        public IQueryable<Author> GetAuthors(bool withTracking = false)
        {
            var initialQuery = _context.Authors
                .Include(x => x.PublishSections)
                .ThenInclude(x => x.Articles)
                    .ThenInclude(al => al.Article)
                    .ThenInclude(ar => ar.Tags);
            
            if (withTracking)
            {
                return initialQuery.AsNoTracking();
            }
            return initialQuery;
        }


        public async Task IncrementPublishedArticleCount(string authorId)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == authorId);
            author.PublishedArticleCount++;

            await _context.SaveChangesAsync();
        }

        public async Task MarkAsRead(Author author, Article article, PaymentTransaction correspondingTransaction)
        {
            author.UnlockedArticles.Add(new ArticleUnlock
            {
                Id = Guid.NewGuid().ToString(),
                AquisitionDate = DateTime.UtcNow,
                AquisitionPrice = article.Price,
                ArticleId = article.ArticleId,
                Author = author,
                //CorrespondingTransaction = correspondingTransaction
            });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateSettings(string authorId, FEUserSettings userSettings)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == authorId);

            author.RequireConfirmationAbove = userSettings.RequireConfirmationAbove;
            author.About = userSettings.About;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateReadArticles(string authorId, IEnumerable<string> unlockedIds)
        {
            var author = await _context.Authors
                .Include(x => x.UnlockedArticles).FirstOrDefaultAsync(x => x.AuthorId == authorId);

            var toRemoveList = author.UnlockedArticles.Where(x => !unlockedIds.Contains(x.Id)).ToList();

            foreach (var toRemove in toRemoveList)
            {
                author.UnlockedArticles.Remove(toRemove);
            }

            //var articlesUnlocked = await _context.Authors.Where(x => x.AuthorId == authorId)
            //    .Include(x => x.UnlockedArticles)
            //    .SelectMany(x => x.UnlockedArticles)
            //    .ToListAsync();

            //articlesUnlocked.RemoveAll(x => !unlockedIds.Contains(x.Id));

            await _context.SaveChangesAsync();
        }

        public async Task<Author> GetAuthorAsync(string id)
        {
            return await GetAuthor(id).SingleOrDefaultAsync();
        }
    }

    public interface IAuthorRepository
    {
        Task<Author> CreateAsync(AuthorCommand command);

        IQueryable<Author> GetAuthorWithArticles(string id);

        IQueryable<Author> GetAuthorWithReadArticles(string id);

        IQueryable<Author> GetAuthor(string id);

        Task<Author> GetAuthorAsync(string id);

        IQueryable<Author> GetAuthorByName(string name);

        IQueryable<Author> GetAuthors(bool withTracking = false);

        IQueryable<Section> GetAuthorSections(string authorId);

        Task MarkAsRead(Author author, Article article, PaymentTransaction correspondingTransaction);

        // TODO - rename userSettings object to something else;
        // Should be fine to use it directly, as it's simply a representation of the data from the DB
        Task UpdateSettings(string authorId, FEUserSettings userSettings);

        Task UpdateReadArticles(string authorId, IEnumerable<string> unlockedIds);

        Task IncrementPublishedArticleCount(string authorId);
    }

    public class AuthorCommand
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Wallet MainWallet { get; set; }

        public Wallet EscrowWallet { get; set; }
    }
}

