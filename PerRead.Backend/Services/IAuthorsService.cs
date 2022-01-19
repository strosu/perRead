using System;
using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Models;
using PerRead.Backend.Models.FrontendModels;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<FEAuthor> GetAuthorAsync(string id)
        {
            var author = _authorRepository.GetAuthorAsync(id);

            return await author.Select(a => new FEAuthor
            {
                Name = a.Name,
                Articles = a.Articles.Select(x => new FEArticleDescription
                {
                    ArticleId = x.ArticleId,
                    Price = x.Article.Price,
                    Title = x.Article.Title,
                    Tags = x.Article.Tags.Select(tag => new FETagDescription
                    {
                        Name = tag.TagName,
                        TagId = tag.TagId
                    })
                })
            }).FirstOrDefaultAsync();
        }

        public async Task<FEAuthor> GetAuthorByNameAsync(string name)
        {
            var author = await _authorRepository.GetAuthorByNameAsync(name);
            return new FEAuthor
            {
                Name = author.Name,
                Articles = author.Articles.Select(art => new FEArticleDescription
                {
                    ArticleId = art.ArticleId,
                    Price = art.Article.Price,
                    Title = art.Article.Title,
                    Tags = art.Article.Tags.Select(tag => new FETagDescription
                    {
                        TagId = tag.TagId,
                        Name = tag.TagName
                    })
                })
            };
        }

        public async Task<IEnumerable<FEAuthor>> GetAuthorsAsync()
        {
            var authors = _authorRepository.GetAuthors();

            return await authors.Select(x => new FEAuthor
            {
                Articles = x.Articles.Select(article => new FEArticleDescription
                {
                    ArticleId = article.ArticleId,
                    Price = article.Article.Price,
                    Title = article.Article.Title,
                    Tags = article.Article.Tags.Select(tag => new FETagDescription
                    {
                        Name = tag.TagName,
                        TagId = tag.TagId
                    })
                })
            }).ToListAsync();
        }
    }

    public interface IAuthorsService
    {
        Task<FEAuthor> GetAuthorAsync(string id);

        Task<FEAuthor> GetAuthorByNameAsync(string name);

        Task<IEnumerable<FEAuthor>> GetAuthorsAsync();
    }
}

