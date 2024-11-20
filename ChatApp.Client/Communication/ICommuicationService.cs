using ChatApp.Shared.Events;

namespace ChatApp.Client.Wpf.Communication;

public interface ICommunicationService
{
    public Task HandleCommunicationAsync();
    public Task SendEventToServer<T>(Event<T> eventToSend);

    public void SendChatDataToServer();
}