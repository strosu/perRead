using PerRead.Backend.Models.Commands;

namespace PerRead.Backend.Services.Requests
{
    public abstract class RequestState
    {
        public void EditRequest(RequestCommand requestCommand) => throw new InvalidOperationException();

        public void AcceptRequest(string requestId) => throw new InvalidOperationException();

        public void CompleteRequest() => throw new InvalidOperationException();

        public void AbandonRequest() => throw new InvalidOperationException();
    }
}
