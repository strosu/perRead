using Microsoft.EntityFrameworkCore;
using PerRead.Backend.Extensions;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;


namespace PerRead.Backend.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<FEArticlePreview>> GetAll();

        Task<FEArticle?> Get(string id);

        Task<FEArticle> Create(ArticleCommand article);

        Task Delete(string id);
    }

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ITagRepository _tagRespository;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISectionRepository _sectionRepository;

        public ArticleService(
            IArticleRepository articleRepository,
            IAuthorRepository authorRepository,
            ITagRepository tagRespository,
            IImageService imageService,
            IHttpContextAccessor httpContextAccessor,
            ISectionRepository sectionRepository)
        {
            _articleRepository = articleRepository;
            _authorRepository = authorRepository;
            _tagRespository = tagRespository;
            _imageService = imageService;
            _httpContextAccessor = httpContextAccessor;
            _sectionRepository = sectionRepository;
        }

        public async Task<FEArticle> Create(ArticleCommand article)
        {
            article.CheckValid();

            var authorId = _httpContextAccessor.GetUserId();

            var author = await _authorRepository.GetAuthor(authorId).FirstOrDefaultAsync();

            if (author == null)
            {
                throw new ArgumentException("Only existing users are allowed to create articles");
            }

            // Ensure the tags are created
            var tagTasks = article.Tags.Select(tag => _tagRespository.GetOrCreate(tag)).ToList();
            await Task.WhenAll(tagTasks);

            // Save the image somewhere usefull and get its path
            var path = await _imageService.Save(author.AuthorId, article.ArticleImage);

            var sections = await _sectionRepository.GetAllSections().Where(x => article.SectionIds.Contains(x.SectionId)).ToListAsync();

            // Create the article itself
            var articleModel = await _articleRepository.Create(author, tagTasks.Select(tagTask => tagTask.Result), sections, path, article);
            return articleModel.ToFEArticle();
        }

        public async Task<IEnumerable<FEArticlePreview>> GetAll()
        {
            var articles = _articleRepository.GetAll();

            var authorId = _httpContextAccessor.GetUserId();
            var requester = await _authorRepository.GetAuthorWithReadArticles(authorId).SingleAsync();

            return await articles.Select(article => article.ToFEArticlePreview(requester)).ToListAsync();
        }

        public async Task<FEArticle?> Get(string id)
        {
            var article = await _articleRepository.Get(id);
            return article?.ToFEArticle();
        }

        public async Task Delete(string id)
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

