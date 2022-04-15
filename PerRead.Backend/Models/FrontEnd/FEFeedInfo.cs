using System;
namespace PerRead.Backend.Models.FrontEnd
{
    public class FEFeedInfo
    {
        public string FeedId { get; set; }

        public string FeedName { get; set; }

        // TODO - maybe return a simpler object that FEAuthor?
        public IEnumerable<FEAuthor> SubscribedAuthors { get; set; }
    }
}

