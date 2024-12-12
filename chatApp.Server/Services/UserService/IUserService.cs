using chatApp_server.Events;
using ChatApp.Shared.Connection;

namespace chatApp_server.User;

public interface IUserService
{
    public void OnUserInformationReceived(object? sender, UserEventArgs userEventArgs);
    public Task RequestUserInformationForAsync(IConnection clientConnection);
    public Task HandleUserInformationAsync(IConnection clientConnection, ChatApp.Shared.User.User user);
}