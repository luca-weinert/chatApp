using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.Services.MessageService;

public interface IMessageService
{
    public Task SendMessageAsync(Message message);
}