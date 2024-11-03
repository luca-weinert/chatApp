namespace chatApp_server.User;

public interface IUserService
{
    public Task RequestUserInformationForAsync(ChatApp.Shared.Connection clientConnection);
    public Task HandleUserInformationAsync(ChatApp.Shared.User.User user);
}