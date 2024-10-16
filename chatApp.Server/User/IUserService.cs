using ChatApp.Shared;

namespace chatApp_server.user;

public interface IUserService
{
    public Task<User> GetUserInformation(Connection.Connection connection);
}