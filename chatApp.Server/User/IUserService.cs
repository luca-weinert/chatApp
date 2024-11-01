namespace chatApp_server.User;

public interface IUserService
{
    public Task<ChatApp.Shared.User.User> GetUserInformation(Connection.ClientConnection clientConnection);
}