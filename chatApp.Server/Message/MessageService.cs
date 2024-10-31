using System.Text;
using System.Text.Json;

namespace chatApp_server.Message;

public class MessageService : IMessageService
{
    public async Task HandleIncomingMessages(Connection.Connection connection)
    {
        var buffer = new byte[1_024];
        try
        {
            var receivedBytes = await connection.NetworkStream.ReadAsync(buffer);
            var receivedString = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
            
            if (string.IsNullOrWhiteSpace(receivedString))
                throw new InvalidOperationException("Received data is empty or invalid.");

            Console.WriteLine($"Received message: {receivedString}");
            
            var message = JsonSerializer.Deserialize<ChatApp.Shared.Message>(receivedString) 
                   ?? throw new InvalidOperationException("Failed to deserialize the user.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}