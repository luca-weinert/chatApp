using chatApp_server.User;
using ChatApp.Communication;
using ICommunicationService = ChatApp.Client.Wpf.Communication.ICommunicationService;

namespace ChatApp.Client.Wpf.User;

public class AuthenticationService : IAuthenticationService
{
    private readonly IEventFactory _eventFactory;
    
    public AuthenticationService(IEventFactory eventFactory)
    {
        _eventFactory = eventFactory;
    }
    
    public async Task AuthenticateUserAsync()
    {
        // var testUSer = new Shared.User.User("Luca Weinert");
        // var userInformationResponseEvent = _eventFactory.CreateUserInformationResponseEvent(testUSer);
        // await _communicationService.SendEventToServer(userInformationResponseEvent);
    }

    public Task<Shared.User.User> GetUserInformationAsync()
    {
        var testUSer = new Shared.User.User("Luca Weinert");
        return Task.FromResult<Shared.User.User>(testUSer);
    }
}