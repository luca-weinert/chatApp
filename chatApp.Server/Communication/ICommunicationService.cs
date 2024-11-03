using chatApp_server.Connection;

namespace chatApp_server.Communication;

public interface ICommunicationService
{
    public Task HandleCommunicationAsync(ChatApp.Shared.Connection clientConnection, CancellationToken cancellationToken);
    public Task<string> ReadFromConnectionAsync(ChatApp.Shared.Connection clientConnection, CancellationToken cancellationToken);
    public Task WriteOnConnectionAsync(ChatApp.Shared.Connection clientConnection, string message);
}