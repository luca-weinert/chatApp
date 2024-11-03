using ChatApp.Shared.Connection;

namespace ChatApp.Communication.Event;

public interface IEventService
{
    public Task HandleEventAsync<T>(Event<T> eEvent, IConnection connection);
    public Task SendEventToAsync<T>(IConnection clientConnection, Event<T> eventToSend);
}