using chatApp_server.Events;
using ChatApp.Shared.Model.Connection;
using ChatApp.Shared.Model.User;

namespace chatApp_server.Services.UserService;

public interface IUserService
{
    public void OnUserInformationReceived(object? sender, UserEventArgs userEventArgs);
    public Task RequestUserInformationForAsync(IConnection clientConnection);
    public Task HandleUserInformationAsync(IConnection clientConnection, User user);
}