using System.Text.Json;
using chatApp_server.User;
using ChatApp.Communication.Events;
using ChatApp.Shared.Connection;

namespace chatApp_server.Events;

public class EventService : IEventService
{
    public EventService()
    {
    }

    public event MessageReceivedEventHandler? MessageReceived;

    public async Task HandleEventAsync<T>(Event<T> eEvent, IConnection clientConnection)
    {
        switch (eEvent.EventType)
        {
            case EventType.MessageReceived:
                Console.WriteLine("[Server]: received received message event");
                break;
            case EventType.MessageRead:
                Console.WriteLine("[Server]: received message read event");
                break;
            case EventType.SendMessage:
                Console.WriteLine("[Server]: received send message event");
                OnMessageSend();
                break;
            case EventType.UserInformationRequest:
                Console.WriteLine("[Server]: received user information request event]");
                break;
            case EventType.UserInformationResponse:
                Console.WriteLine("[Server]: received user information event");
                break; 
            default:
                Console.WriteLine("[Server]: received unknown event type");
                throw new ArgumentOutOfRangeException();
        }
    }

    protected virtual void OnMessageSend()
    {
         MessageReceived?.Invoke(this, EventArgs.Empty);
    }
    
    public async Task SendEventToAsync<T>(IConnection clientConnection, Event<T> eventToSend)
    {
        var serializedEvent = JsonSerializer.Serialize(eventToSend);
        await clientConnection.WriteAsync(serializedEvent);
    }
}