using ChatApp.Shared.Connection;
using ChatApp.Shared.Events;

namespace ChatApp.Client.Wpf.Event;

public interface IEventService
{
    public event EventHandler MessageReceivedEvent;
    public event EventHandler<MessageEventArgs> MessageSentEvent;
    public event EventHandler UserInformationReceivedEvent;
    public Task HandleEventAsync<T>(Event<T> incomingEvent, IConnection connection);
    public Task SendEventToAsync<T>(IConnection clientConnection, Event<T> eventToSend);
}