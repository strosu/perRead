using System;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class OwnershipExtensions
    {
        public static FEArticleOwner ToFEArticleOwner(this ArticleOwner owner)
        {
            return new FEArticleOwner
            {
                OwnerPreview = owner.Author.ToFEAuthorPreview(),
                OwnershipPercent = owner.OwningPercentage * 100,
                CanBeEdited = owner.CanBeEdited,
                IsUserFacing = owner.IsUserFacing
            };
        }

        public static FEArticleOwnership ToFEArticleOwnership(this Article article)
        {
            return new FEArticleOwnership
            {
                Owners = article.ArticleOwners.Select(x => x.ToFEArticleOwner())
            };
        }
    }
}

