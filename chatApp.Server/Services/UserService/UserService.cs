using ChatApp.Server.Models.Connection;
using ChatApp.Server.Repositories.Connection;
using ChatApp.Server.Repositories.User;
using ChatApp.Shared.Events;
using ChatApp.Shared.Model.User;

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
        _userRepository.SaveUserAsync(userEventArgs.User);
        var userRelatedConnection = await _connectionRepository.GetConnectionByConnectionId(userEventArgs.ConnectionID);
        userRelatedConnection.UserId = userEventArgs.User.Id;
        _connectionRepository.UpdateConnection(userRelatedConnection);
    }
    
    public Task RequestUserInformationForAsync(ClientConnection clientConnection)
    {
        return Task.CompletedTask;
    }
    
    public Task HandleUserInformationAsync(ClientConnection clientConnection, User user)
    {
        return Task.CompletedTask;
    }
}