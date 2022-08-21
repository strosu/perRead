using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Extensions;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Services.Articles
{
    public interface IArticleRecommendService
    {
        Task<FEArticle> ApplyRecommendation(string id, ArticleRecommendCommand command);

        Task<FEArticle> Recommend(string id);

        Task<FEArticle> DoNotRecommend(string id);

        Task<FEArticle> ClearRecommendation(string id);
    }

    public class ArticleRecommendService : IArticleRecommendService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IRequesterGetter _requesterGetter;

        public ArticleRecommendService(IRequesterGetter requesterGetter, IArticleRepository articleRepository)
        {
            _requesterGetter = requesterGetter;
            _articleRepository = articleRepository;
        }

        public async Task<FEArticle> Recommend(string articleId)
        {
            return await SetRecommendation(articleId, true);
        }

        public async Task<FEArticle> DoNotRecommend(string articleId)
        {
            return await SetRecommendation(articleId, false);
        }

        public async Task<FEArticle> ClearRecommendation(string articleId)
        {
            return await SetRecommendation(articleId, null);
        }

        private async Task<FEArticle> SetRecommendation(string articleId, bool? state)
        {
            var requester = await _requesterGetter.GetRequester();
            var article = await _articleRepository.SetReview(articleId, requester.AuthorId, state);
            return article.ToFEArticle(requester);
        }

        public async Task<FEArticle> ApplyRecommendation(string id, ArticleRecommendCommand command)
        {
            switch (command) {
                case ArticleRecommendCommand.Clear:
                    return await ClearRecommendation(id);
                case ArticleRecommendCommand.Yes:
                    return await Recommend(id);
                case ArticleRecommendCommand.Not:
                    return await DoNotRecommend(id);
                default:
                    throw new ArgumentException("Could not understand the command");
            }
        }
    }

    public enum ArticleRecommendCommand
    {
        Clear,
        Yes,
        Not
    }
}
