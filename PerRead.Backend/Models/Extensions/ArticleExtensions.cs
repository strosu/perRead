using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class ArticleExtensions
    {
        public static FEArticleUnlockInfo ToFEArticleUnlockInfo(this ArticleUnlock articleUnlock)
        {
            return new FEArticleUnlockInfo
            {
                Article = articleUnlock.Article.ToFEArticlePreview(),
                ArticleUnlockId = articleUnlock.Id,
                AquisitionDate = articleUnlock.AquisitionDate,
                AquisitionPrice = articleUnlock.AquisitionPrice,
            };
        }
    }
}
