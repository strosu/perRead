using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Services.Articles
{
    public interface IArticleRecommendService
    {
        Task<Article> ApplyRecommendation(string id, ArticleRecommendCommand command);

        Task<Article> Recommend(string id);

        Task<Article> DoNotRecommend(string id);

        Task<Article> ClearRecommendation(string id);
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

        public async Task<Article> Recommend(string articleId)
        {
            return await SetRecommendation(articleId, true);
        }

        public async Task<Article> DoNotRecommend(string articleId)
        {
            return await SetRecommendation(articleId, false);
        }

        public async Task<Article> ClearRecommendation(string articleId)
        {
            return await SetRecommendation(articleId, null);
        }

        private async Task<Article> SetRecommendation(string articleId, bool? state)
        {
            var requester = await _requesterGetter.GetRequester();
            return await _articleRepository.SetReview(articleId, requester.AuthorId, false);
        }

        public async Task<Article> ApplyRecommendation(string id, ArticleRecommendCommand command)
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
