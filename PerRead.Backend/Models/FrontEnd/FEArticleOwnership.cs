using System;
namespace PerRead.Backend.Models.FrontEnd
{
    public class FEArticleOwnership
    {
        public IEnumerable<FEArticleOwner> Owners { get; set; }
    }

    public class FEArticleOwner
    {
        public FEAuthorPreview OwnerPreview { get; set; }

        // TODO - return as percentages, or 0.x?
        public double OwnershipPercent { get; set; }

        public bool CanBeEdited { get; set; }

        public bool IsUserFacing { get; set; }
    }
}

