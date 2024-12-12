using ChatApp.Server.Events;
using ChatApp.Server.Models.Connection;
using ChatApp.Shared.Model.Connection;
using ChatApp.Shared.Model.User;

namespace ChatApp.Server.Services.UserService;

public interface IUserService
{
    public void OnUserInformationReceived(object? sender, UserEventArgs userEventArgs);
    public Task RequestUserInformationForAsync(IClientConnection clientConnection);
    public Task HandleUserInformationAsync(IClientConnection clientConnection, User user);
}