using chatApp_server.Connection;
using chatApp_server.Connection.Services;
using chatApp_server.Events;
using ChatApp.Shared.Connection;

namespace chatApp_server.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConnectionService _connectionService;
    
    public UserService(IUserRepository userRepository, IConnectionService connectionService)
    {
        _userRepository = userRepository;
        _connectionService = connectionService;
    }
    
    public void OnUserInformationReceived(object? sender, UserEventArgs userEventArgs)
    {
        // save received user information in the user repository
        _userRepository.SaveUserAsync(userEventArgs.User);
        
        // get the connection related to the current user
        var connection = userEventArgs.Connection;
        
        // set the userId in the connection to the id of the current user
        connection.UserId = userEventArgs.User.Id;
        
        // update the connection with the new received userid (that is related to this connection)
        _connectionService.UpdateConnection(connection);
    }
    
    public Task RequestUserInformationForAsync(IConnection clientConnection)
    {
        return Task.CompletedTask;
    }
    
    public Task HandleUserInformationAsync(IConnection clientConnection, ChatApp.Shared.User.User user)
    {
        return Task.CompletedTask;
    }
}