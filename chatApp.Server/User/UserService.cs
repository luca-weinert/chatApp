using chatApp_server.Communication;
using chatApp_server.Connection;
using ChatApp.Communication;

namespace chatApp_server.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICommunicationService _communicationService;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task RequestUserInformationForAsync(ChatApp.Shared.Connection.Connection clientConnection)
    {
        var requestUserInformation = new Event<object>(EventType.UserInformationRequest, null);
    }

    public async Task HandleUserInformationAsync(ChatApp.Shared.User.User user)
    {
        await _userRepository.SaveUserAsync(user);
        Console.WriteLine($"[Server]: User information for {user.Name} saved successfully.");
    }
}