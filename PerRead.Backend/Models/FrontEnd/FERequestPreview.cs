using System;
using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Models.FrontEnd
{
    public class FERequestPreview
    {
        public string RequestId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public FEAuthorPreview TargetAuthor { get; set; }

        public int PledgeCount { get; set; }

        public long PledgeAmount { get; set; }

        public RequestState RequestState { get; set; }

        public RequestPostPublishState PostPublishState { get; set; }

        public FEArticlePreview ResultingArticle { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

