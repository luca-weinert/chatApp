using System.Text.Json;
using chatApp_server.User;
using ChatApp.Communication.Event;
using ChatApp.Shared.Connection;

namespace chatApp_server.Event;

public class EventService : IEventService
{
    private readonly IUserService _userService;
    
    public EventService(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task HandleEventAsync<T>(Event<T> eEvent, IConnection clientConnection)
    {
        switch (eEvent.EventType)
        {
            case EventType.MessageReceived:
                Console.WriteLine("[Server]: received received message event]");
                break;
            case EventType.MessageRead:
                Console.WriteLine("[Server]: received message read event]");
                break;
            case EventType.SendMessage:
                Console.WriteLine("[Server]: received send message event]");
                break;
            case EventType.UserInformationRequest:
                Console.WriteLine("[Server]: received user information request event]");
                break;
            case EventType.UserInformationResponse:
                Console.WriteLine("[Server]: received user information event");

                if (eEvent.Payload is ChatApp.Shared.User.User user)
                {
                    await _userService.HandleUserInformationAsync(clientConnection, user);
                }
                else
                {
                    Console.WriteLine("[Server]: Invalid payload type for UserInformationResponse event");
                }
                break;
            default:
                Console.WriteLine("[Server]: received unknown event type");
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public async Task SendEventToAsync<T>(IConnection clientConnection, Event<T> eventToSend)
    {
        var serializedEvent = JsonSerializer.Serialize(eventToSend);
        await clientConnection.WriteAsync(serializedEvent);
    }
}