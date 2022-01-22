﻿using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;


namespace PerRead.Backend.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<FEArticlePreview>> GetAll();

        Task<FEArticle?> Get(int id);

        Task<FEArticle> Create(ArticleCommand article);

        Task Delete(int id);
    }

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ITagRepository _tagRespository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticleService(
            IArticleRepository articleRepository, 
            IAuthorRepository authorRepository,
            ITagRepository tagRespository,
            IHttpContextAccessor httpContextAccessor)
        {
            _articleRepository = articleRepository;
            _authorRepository = authorRepository;
            _tagRespository = tagRespository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<FEArticle> Create(ArticleCommand article)
        {
            article.CheckValid();

            var authorName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            var author = await _authorRepository.GetAuthorAsync(authorName).FirstOrDefaultAsync();

            if (author == null)
            {
                throw new ArgumentException("Only existing users are allowed to create articles");
            }

            var tagTasks = article.Tags.Select(tag => _tagRespository.GetOrCreate(tag)).ToList();
            await Task.WhenAll(tagTasks);

            var articleModel = await _articleRepository.Create(author, tagTasks.Select(tagTask => tagTask.Result), article);
            return articleModel.ToFEArticle();
        }

        public async Task<IEnumerable<FEArticlePreview>> GetAll()
        {
            var articles = _articleRepository.GetAll();
            return await articles.Select(article => article.ToFEArticlePreview()).ToListAsync();
        }

        public async Task<FEArticle?> Get(int id)
        {
            var article = await _articleRepository.Get(id);
            return article?.ToFEArticle();
        }

        public async Task Delete(int id)
        {
            var article = await _articleRepository.Get(id);

            if (article == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            await _articleRepository.Delete(id);
        }
    }
}

