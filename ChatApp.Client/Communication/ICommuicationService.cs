namespace ChatApp.Client.Wpf.Communication;

public interface ICommunicationService
{
    public Task HandleCommunicationAsync();

    public void SendChatDataToServer();
}