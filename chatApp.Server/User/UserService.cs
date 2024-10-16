using System.Text;
using System.Text.Json;
using ChatApp.Shared;

namespace chatApp_server.user;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserInformation(Connection.Connection connection)
    {
        var buffer = new byte[1024];
        try
        {
            var receivedBytes = await connection.NetworkStream.ReadAsync(buffer);
            var receivedString = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

            if (string.IsNullOrWhiteSpace(receivedString))
                throw new InvalidOperationException("Received data is empty or invalid.");

            return JsonSerializer.Deserialize<User>(receivedString) 
                   ?? throw new InvalidOperationException("Failed to deserialize the user.");
        }
        catch (JsonException ex)
        {
            throw new InvalidDataException("Failed to deserialize user information", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while receiving user information", ex);
        }
    }
}