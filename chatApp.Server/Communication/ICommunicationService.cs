using chatApp_server.Connection;

namespace chatApp_server.Communication;

public interface ICommunicationService
{
    public Task HandleCommunicationForAsync(ClientConnection clientConnection);
}