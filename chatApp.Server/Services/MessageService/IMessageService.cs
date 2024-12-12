using ChatApp.Server.Events;

namespace ChatApp.Server.Services.MessageService;

public interface IMessageService
{
    public void OnMessageReceived(object? sender, MessageEventArgs e);
}