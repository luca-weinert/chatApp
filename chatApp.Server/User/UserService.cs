using ChatApp.Shared.Connection;

namespace chatApp_server.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task RequestUserInformationForAsync(IConnection clientConnection)
    {
    }
    
    public async Task HandleUserInformationAsync(IConnection clientConnection, ChatApp.Shared.User.User user)
    {
    }
}