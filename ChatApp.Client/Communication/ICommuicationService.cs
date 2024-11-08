using ChatApp.Communication.Events;

namespace ChatApp.Client.Wpf.Communication;

public interface ICommunicationService
{
    public Task HandleCommunicationAsync();
    public Task SendEventToServer<T>(Event<T> eventToSend);
}