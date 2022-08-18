using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Models.BusinessRules
{
    public static class RequestRules
    {
        public static bool IsEditableBy(this ArticleRequest request, Author requester)
        {
            if (request.RequestState != RequestState.Created)
            {
                return false;
            }

            // Business rules, maybe move somewhere else
            // A request is editable only if all the pledges are done by a single user, and that user is the current one
            var pledgingUsers = request.Pledges.Select(x => x.Pledger.AuthorId);

            return pledgingUsers.Count() == 1 && pledgingUsers.First() == requester.AuthorId;
        }

        public static bool AcceptsNewPledges(ArticleRequest request)
        {
            return request.RequestState == RequestState.Created;
        }

        public static bool IsEditableByCurrentUser(RequestPledge pledge, Author requester)
        {
            if (pledge.ParentRequest.RequestState != RequestState.Created)
            {
                return false;   
            }

            return pledge.Pledger.AuthorId == requester.AuthorId;
        }
    }
}
