namespace chatApp_server.Message;

public interface IMessageService
{
    public void OnMessageSend(object source, EventArgs args);
    public Task HandleIncomingMessagesAsync(ChatApp.Shared.Connection.Connection clientConnection);
    public Task ProcessMessageAsync(ChatApp.Shared.Message.Message message);
    public Task HandleOutgoingMessages(ChatApp.Shared.Message.Message message);
}