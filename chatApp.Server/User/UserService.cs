using System.Text;
using System.Text.Json;

namespace chatApp_server.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ChatApp.Shared.User.User> GetUserInformation(Connection.ClientConnection clientConnection)
    {
        var buffer = new byte[1024];
        try
        {
            var receivedBytes = await clientConnection.NetworkStream.ReadAsync(buffer);
            var receivedString = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

            if (string.IsNullOrWhiteSpace(receivedString))
                throw new InvalidOperationException("Received data is empty or invalid.");

            return JsonSerializer.Deserialize<ChatApp.Shared.User.User>(receivedString) 
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