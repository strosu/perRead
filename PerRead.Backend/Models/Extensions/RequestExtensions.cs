using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class RequestExtensions
    {
        public static FERequest ToFERequest(this ArticleRequest request)
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
                ResultingArticle = request.ResultingArticle
            };
        }

        public static FERequestPreview ToFERequestPreview(this ArticleRequest request)
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
                ResultingArticle = request.ResultingArticle
            };
        }
    }
}

