using ChatApp.Shared.Connection;
using ChatApp.Shared.Events;

namespace ChatApp.Client.Wpf.Event;

public class EventService : IEventService
{
    public event EventHandler? MessageReceivedEvent;
    public event EventHandler<MessageEventArgs>? MessageSentEvent;
    public event EventHandler? UserInformationReceivedEvent;

    public Task HandleEventAsync<T>(Event<T> incomingEvent, IConnection connection)
    {
        switch (incomingEvent.EventType)
        {
            case EventType.MessageReceived:
                Console.WriteLine("[Client]: received received message event]");
                break;
            case EventType.MessageRead:
                Console.WriteLine("[Client]: received message read event]");
                break;
            case EventType.SendMessage:
                Console.WriteLine("[Client]: received send message event]");
                break;
            case EventType.UserInformationRequest:
                Console.WriteLine("[Client]: received user information request event]");
                break;
            case EventType.UserInformation:
                Console.WriteLine("[Client]: received user information event");
                break;
            default:
                Console.WriteLine("[Client]: received unknown event type");
                throw new ArgumentOutOfRangeException();
        }

        return Task.CompletedTask;
    }

    public Task SendEventToAsync<T>(IConnection clientConnection, Event<T> eventToSend)
    {
        throw new NotImplementedException();
    }
}