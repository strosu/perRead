using PerRead.Backend.Models.BackEnd;

namespace PerRead.Backend.Models.BusinessRules
{
    public static class RequestRules
    {
        public static bool IsEditable(ArticleRequest request, Author requester)
        {
            // Business rules, maybe move somewhere else
            // A request is editable only if all the pledges are done by a single user, and that user is the current one
            var pledgingUsers = request.Pledges.Select(x => x.Pledger.AuthorId);

            return pledgingUsers.Count() == 1 && pledgingUsers.First() == requester.AuthorId;
        }
    }
}
