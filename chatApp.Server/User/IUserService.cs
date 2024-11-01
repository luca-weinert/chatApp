using chatApp_server.Connection;

namespace chatApp_server.User;

public interface IUserService
{
    public Task RequestUserInformationForAsync(ClientConnection clientConnection);
    public Task HandleUserInformation();
}