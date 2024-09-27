namespace chatApp_server.Message;

public interface IMessageService
{
    public void handleIncomingMessages(ChatApp.Shared.Message message);
}