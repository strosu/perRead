﻿using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.FrontendModels;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<FEArticleDescription>> GetAll();

        Task<Article?> Get(int id);

        Task<Article?> Create(ArticleCommand article);

        Task Delete(int id);
    }

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<Article?> Create(ArticleCommand article)
        {
            // Business logic
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            if (article.Authors == null || !article.Authors.Any())
            {
                throw new ArgumentNullException(nameof(article.Authors));
            }

            if (article.Content == null)
            {
                throw new ArgumentNullException(nameof(article.Content));
            }

            if (article.Tags == null || !article.Tags.Any())
            {
                throw new ArgumentException("Each article requires at least one tag");
            }

            return await _articleRepository.Create(article);
        }

        public async Task<IEnumerable<FEArticleDescription>> GetAll()
        {
            await _articleRepository.EnsureSeeded();

            var articles = _articleRepository.GetAll();

            return await articles.Select(x => new FEArticleDescription
            {
                ArticleId = x.ArticleId,
                Title = x.Title,
                Price = x.Price,
                Tags = x.Tags.Select(t => new FETagDescription
                {
                    Name = t.TagName,
                    TagUrl = t.TagId.ToString()
                }),
                Authors = x.ArticleAuthors.Select(a => new FEAuthorDescription
                {
                    Name = a.Author.Name,
                    ProfileUrl = a.AuthorId.ToString()
                })
            }).ToListAsync();
        }

        public async Task<Article?> Get(int id)
        {
            return await _articleRepository.Get(id);

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

