using ChatApp.Shared.Connection;

namespace ChatApp.Communication;

public interface ICommunicationService
{
    public Task HandleCommunicationAsync(Connection clientConnection, CancellationToken cancellationToken);
    public Task SendEventToClientAsync<T>(Connection clientConnection, Event<T> eventToSend);
}