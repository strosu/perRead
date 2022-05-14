using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.Extensions;

namespace PerRead.Backend.Models.FrontEnd
{
    public static class FrontEndModelExtensions
    {
        public static FETag ToFETag(this Tag tagModel, Author requester)
        {
            return new FETag
            {
                Id = tagModel.TagId,
                Name = tagModel.TagName,
                ArticlePreviews = tagModel.Articles?.Select(x => x.ToFEArticlePreview(requester)),
                FirstUsage = tagModel.FirstUsage,
            };
        }

        public static FETagPreview ToFETagPreview(this Tag tagModel)
        {
            return new FETagPreview
            {
                TagId = tagModel.TagId,
                TagName = tagModel.TagName
            };
        }


    }
}
