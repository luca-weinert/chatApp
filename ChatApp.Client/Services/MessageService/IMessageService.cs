namespace ChatApp.Client.Wpf.Services.Message;

public interface IMessageService
{
    public Task SendMessageAsync(Shared.Message.Message message);
}