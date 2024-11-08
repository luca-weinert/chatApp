using ChatApp.Shared.Connection;

namespace ChatApp.Communication.Events;
public delegate void MessageReceivedEventHandler(object source, EventArgs args);

public interface IEventService
{
    public event MessageReceivedEventHandler MessageReceived;
    public Task HandleEventAsync<T>(Event<T> eEvent, IConnection connection);
    public Task SendEventToAsync<T>(IConnection clientConnection, Event<T> eventToSend);
}