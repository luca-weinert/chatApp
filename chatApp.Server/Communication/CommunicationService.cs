using System.Text;
using System.Text.Json;
using chatApp_server.Connection;
using chatApp_server.User;
using ChatApp.Communication;

namespace chatApp_server.Communication;

public class CommunicationService : ICommunicationService
{
    private readonly IUserService _userService;
    private readonly IEventSender _eventSender;

    public CommunicationService(IUserService userService, IEventSender eventSender)
    {
        _userService = userService;
        _eventSender = eventSender;
    }
    public async Task HandleCommunicationForAsync(ClientConnection clientConnection)
    {
        var userInformationRequestEvent = new Event<object>(EventType.UserInformationRequest, null);
        await _eventSender.SendEventAsync(userInformationRequestEvent, clientConnection.NetworkStream);
    }
}