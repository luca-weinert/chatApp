using chatApp_server.Events;

namespace chatApp_server.Message;

public interface IMessageService
{
    public void OnMessageReceived(object? sender, MessageEventArgs e);
}