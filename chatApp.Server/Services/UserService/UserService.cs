using ChatApp.Server.Repositories.Connection;
using ChatApp.Server.Repositories.User;
using ChatApp.Shared.Events;

namespace ChatApp.Server.Services.UserService;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly ConnectionRepository _connectionRepository;
    
    public UserService()
    {
        _userRepository = new UserRepository();
        _connectionRepository = new ConnectionRepository();
    }
    
    public async void OnUserInformationReceived(object? sender, UserEventArgs userEventArgs)
    {
        await _userRepository.SaveUserAsync(userEventArgs.User);
        var userRelatedConnection = await _connectionRepository.GetConnectionByConnectionId(userEventArgs.ConnectionID);
        if (userRelatedConnection == null) return;
        userRelatedConnection.UserId = userEventArgs.User.Id;
        _connectionRepository.UpdateConnection(userRelatedConnection);
    }
}