using ChatApp.Client.Wpf.Communication;
using ChatApp.Communication;

namespace ChatApp.Client.Wpf.User;

public class AuthenticationService : IAuthenticationService
{
    private readonly ICommunicationService _communicationService;
    public readonly IEventFactory _eventFactory;
    public AuthenticationService(ICommunicationService communicationService, IEventFactory eventFactory)
    {
        _communicationService = communicationService;
        _eventFactory = eventFactory;
    }
    
    public async Task AuthenticateUserAsync(Shared.User.User user)
    {
        var userInformationResponseEvent = _eventFactory.CreateUserInformationResponseEvent(user);
        await _communicationService.SendEventToServerAsync(userInformationResponseEvent);
    }
}