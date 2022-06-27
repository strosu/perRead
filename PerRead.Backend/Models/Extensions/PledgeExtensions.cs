using System;
using PerRead.Backend.Models.BackEnd;
using PerRead.Backend.Models.FrontEnd;

namespace PerRead.Backend.Models.Extensions
{
    public static class PledgeExtensions
    {
        public static FEPledgePreview ToFEPledgePreview(this RequestPledge pledge)
        {
            return new FEPledgePreview
            {
                RequestPledgeId = pledge.RequestPledgeId,
                Pledger = pledge.Pledger.ToFEAuthorPreview(),
                TotalTokenSum = pledge.TotalTokenSum
            };
        }
    }
}

