using ChatApp.Server.Events;
using ChatApp.Shared.Model.Connection;
using ChatApp.Shared.Model.User;

namespace ChatApp.Server.Services.UserService;

public interface IUserService
{
    public void OnUserInformationReceived(object? sender, UserEventArgs userEventArgs);
    public Task RequestUserInformationForAsync(IConnection clientConnection);
    public Task HandleUserInformationAsync(IConnection clientConnection, User user);
}