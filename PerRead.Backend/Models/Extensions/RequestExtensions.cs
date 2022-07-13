using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.BusinessRules;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class RequestExtensions
    {
        public static FERequest ToFERequest(this ArticleRequest request, Author requester)
        {
            return new FERequest
            {
                RequestId = request.ArticleRequestId,
                Title = request.Title,
                Description = request.Description,
                TargetAuthor = request.TargetAuthor.ToFEAuthorPreview(),
                PledgeCount = request.Pledges.Count,
                PledgeAmount = request.Pledges.Sum(x => x.TotalTokenSum),
                Deadline = request.Deadline,
                PostPublishState = request.PostPublishState,
                RequestState = request.RequestState,
                ResultingArticle = request.ResultingArticle?.ToFEArticlePreview(requester),
                CreatedAt = request.CreatedAt,
                PledgePreviews = request.Pledges.Select(x => x.ToFEPledgePreview()),
                EditableByCurrentUser = RequestRules.IsEditable(request, requester)
            };
        }

        public static FERequestPreview ToFERequestPreview(this ArticleRequest request, Author requester)
        {
            return new FERequestPreview
            {
                RequestId = request.ArticleRequestId,
                Title = request.Title,
                Description = request.Description,
                TargetAuthor = request.TargetAuthor.ToFEAuthorPreview(),
                PledgeCount = request.Pledges.Count,
                PledgeAmount = request.Pledges.Sum(x => x.TotalTokenSum),
                Deadline = request.Deadline,
                PostPublishState = request.PostPublishState,
                RequestState = request.RequestState,
                ResultingArticle = request.ResultingArticle?.ToFEArticlePreview(requester),
                CreatedAt = request.CreatedAt,
            };
        }
    }
}

