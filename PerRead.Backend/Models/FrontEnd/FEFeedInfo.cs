using System;
namespace PerRead.Backend.Models.FrontEnd
{
    public class FEFeedInfo
    {
        public string FeedId { get; set; }

        public string FeedName { get; set; }

        public IEnumerable<FEAuthorPreview> SubscribedAuthors { get; set; }
    }
}

