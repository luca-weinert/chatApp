using System.Text.Json;
using ChatApp.Shared.Connection;
using ChatApp.Shared.Events;

namespace chatApp_server.Events;

public sealed class EventService : IEventService
{
    public event EventHandler? MessageReceivedEvent;
    public event EventHandler<MessageEventArgs>? MessageSentEvent;
    public event EventHandler? UserInformationReceivedEvent;

    public async Task HandleEventAsync<T>(Event<T>? incomingEvent)
    {
        if (incomingEvent != null)
            switch (incomingEvent.EventType)
            {
                case EventType.MessageReceived:
                    Console.WriteLine("[EventService]: received received message event");
                    break;
                case EventType.MessageRead:
                    Console.WriteLine("[EventService]: received message read event");
                    break;
                case EventType.SendMessage:
                    Console.WriteLine("[EventService]: received send message event");
                    if (incomingEvent.Payload is ChatApp.Shared.Message.Message mgs) 
                        OnMessageSend(new MessageEventArgs(mgs));
                    break;
                case EventType.UserInformationRequest:
                    Console.WriteLine("[EventService]: received user information request event]");
                    break;
                case EventType.UserInformationResponse:
                    Console.WriteLine("[EventService]: received user information event");
                    break;
                default:
                    Console.WriteLine("[EventService]: received unknown event type");
                    throw new ArgumentOutOfRangeException();
            }
    }

    private void OnMessageSend(MessageEventArgs args)
    {
         MessageSentEvent?.Invoke(this, args);
    }
    
    public async Task SendEventToAsync<T>(IConnection clientConnection, Event<T> eventToSend)
    {
        var serializedEvent = JsonSerializer.Serialize(eventToSend);
        await clientConnection.WriteAsync(serializedEvent);
    }
}