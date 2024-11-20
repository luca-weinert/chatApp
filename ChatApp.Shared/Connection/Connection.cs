using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ChatApp.Shared.Connection;

public class Connection : IConnection
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get;  set; }
    public TcpClient Client { get; private set; }
    public Stream NetworkStream { get; private set; }

    public Connection(TcpClient tcpClient)
    {
        Client = tcpClient;
        NetworkStream = tcpClient.GetStream(); // Initialize the NetworkStream based on the TcpClient
    }

    public async Task<string?> ReadAsync(CancellationToken cancellationToken)
    {
        var buffer = new byte[1024];
        var messageBuilder = new StringBuilder();

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Read data from the network stream asynchronously
                var receivedBytes = await NetworkStream.ReadAsync(buffer, cancellationToken);

                if (receivedBytes == 0)
                {
                    // If no bytes are received, the connection may be closed
                    Console.WriteLine("[Connection]: The client has disconnected.");
                    return null; // Indicate that the connection is closed
                }

                // Convert the received bytes into a string
                var jsonString = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                Console.WriteLine($"[Connection]: Data received: {jsonString}");

                messageBuilder.Append(jsonString);

                // Assuming each event is sent as a complete JSON object in one message,
                if (!IsCompleteMessage(jsonString)) continue;
                var completeMessage = messageBuilder.ToString();
                messageBuilder.Clear(); // Clear the buffer for the next message
                return completeMessage;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Connection]: Error while reading data - {ex.Message}");
        }

        return null; 
    }
    
    private bool IsCompleteMessage(string json)
    {
        try
        {
            JsonDocument.Parse(json); 
            return true;
        }
        catch (JsonException)
        {
            return false; 
        }
    }
    
    public async Task<bool> WriteAsync(string data)
    {
        try
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            await NetworkStream.WriteAsync(bytes);
            Console.WriteLine($"[Connection]: Data written: {data}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Connection]: something went wrong while writing: {ex}");
            return false;
        }
    }
}