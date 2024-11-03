using ChatApp.Communication.Event;
using ChatApp.Shared.Connection;

namespace ChatApp.Client.Wpf.Event;

public class EventService : IEventService
{
    public Task HandleEventAsync<T>(Event<T> eEvent, IConnection connection)
    {
        switch (eEvent.EventType)
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
            case EventType.UserInformationResponse:
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