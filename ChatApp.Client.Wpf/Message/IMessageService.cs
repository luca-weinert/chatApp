namespace ChatApp.Client.Wpf.Message;

public interface IMessageService
{
    public Task SendAsync(Shared.Message.Message message);
}