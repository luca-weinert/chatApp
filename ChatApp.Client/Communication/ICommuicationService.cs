using ChatApp.Communication.Event;

namespace ChatApp.Client.Wpf.Communication;

public interface ICommunicationService
{
    public Task HandleCommunicationAsync();
    public Task SendEventToServer<T>(Event<T> eventToSend);
}