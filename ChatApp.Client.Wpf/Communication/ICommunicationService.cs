using ChatApp.Communication;

namespace ChatApp.Client.Wpf.Communication;

public interface ICommunicationService
{
    public Task SendEventToServerAsync<T>(Event<T> eventToSend);
}