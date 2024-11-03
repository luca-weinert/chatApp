using chatApp_server.Message;
using chatApp_server.User;
using ChatApp.Communication;

namespace chatApp_server.Communication;

public class ServerEventHandler : IEventHandler
{
    private readonly IUserService _userService;
    private readonly IEventTransmitter _eventTransmitter;
    
    public ServerEventHandler(IUserService userService, IEventTransmitter eventTransmitter)
    {
        _userService = userService;
        _eventTransmitter = eventTransmitter;
    }
    
    public async Task HandleEventAsync<T>(Event<T> eEvent)
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
                    await _userService.HandleUserInformationAsync(user);
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
}