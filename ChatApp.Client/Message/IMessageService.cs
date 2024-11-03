namespace ChatApp.Client.Wpf.Message;

public interface IMessageService
{
    public Task SendMessageAsync(Shared.Message.Message message);
}