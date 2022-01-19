using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticlePreview>> GetAll();

        Task<Article> Get(int id);

        Task<Article> Create(string author, ArticleCommand article);

        Task Delete(int id);
    }

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<Article> Create(string author, ArticleCommand article)
        {
            // Business logic
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            if (article.Content == null)
            {
                throw new ArgumentNullException(nameof(article.Content));
            }

            if (article.Tags == null || !article.Tags.Any())
            {
                throw new ArgumentException("Each article requires at least one tag");
            }

            var articleModel = await _articleRepository.Create(author, article);
            return articleModel.ToArticle();
        }

        public async Task<IEnumerable<ArticlePreview>> GetAll()
        {
            var articles = _articleRepository.GetAll();
            return await articles.Select(article => article.ToArticlePreview()).ToListAsync();
        }

        public async Task<Article> Get(int id)
        {
            var article = await _articleRepository.Get(id);
            return article.ToArticle();
        }

        public async Task Delete(int id)
        {
            var article = await _articleRepository.Get(id);

            if (article == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await _articleRepository.Delete(article);
        }
    }
}

