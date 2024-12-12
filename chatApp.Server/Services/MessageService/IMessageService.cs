using chatApp_server.Events;

namespace chatApp_server.Services.MessageService;

public interface IMessageService
{
    public void OnMessageReceived(object? sender, MessageEventArgs e);
}