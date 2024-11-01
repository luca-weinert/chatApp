using ChatApp.Client.Wpf.Communication;

namespace ChatApp.Client.Wpf.User;

public class AuthenticationService : IAuthenticationService
{
    private CommunicationService _communicationService;


    public AuthenticationService(CommunicationService communicationService)
    {
        _communicationService = communicationService;
    }
    
    public async Task AuthenticateUserAsync(Shared.User.User user)
    {
        var sendUserEvent = _communicationService.CreateSendUserInformationEvent(user);
        await _communicationService.SendEventToServerAsync(sendUserEvent);
    }
}