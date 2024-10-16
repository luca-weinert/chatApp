using System.Text;
using System.Text.Json;
using ChatApp.Shared;

namespace chatApp_server.user;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User> GetUserInformation(Connection.Connection connection)
    {
        var buffer = new byte[1024];
        var receivedBytes = await connection.NetworkStream.ReadAsync(buffer);
        var receivedString = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
        return JsonSerializer.Deserialize<User>(receivedString);
    }
}