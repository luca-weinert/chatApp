namespace ChatApp.Client.Wpf.Services.Communication;

public interface ICommunicationService
{
    public Task HandleCommunicationAsync();

    public void SendChatDataToServer();
}