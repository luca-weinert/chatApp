namespace chatApp_server.Message;

public interface IMessageService
{
    public Task HandleIncomingMessagesAsync(Connection.ClientConnection clientConnection);
    public Task ProcessMessageAsync(ChatApp.Shared.Message.Message message);
    public Task HandleOutgoingMessages(ChatApp.Shared.Message.Message message);
}