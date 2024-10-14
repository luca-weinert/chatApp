using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using ChatApp.Shared;

namespace chatApp_client;

public abstract class TcpConnection(TcpClient tcpClient)
{
    private readonly NetworkStream _networkStream = tcpClient.GetStream();

    // Asynchronously sends a message to the server
    public async Task SendMessageAsync(string message)
    {
        var messageBytes = Encoding.ASCII.GetBytes(message);
        await _networkStream.WriteAsync(messageBytes);
    }

    // Asynchronously receives messages from the server
    public async Task ReceiveMessageAsync()
    {
        var buffer = new byte[1024];

        try
        {
            while (true)
            {
                var bytesRead = await _networkStream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    // Server has closed the connection
                    Console.WriteLine("Disconnected from server.");
                    break;
                }

                // Process the received message
                var rawMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                try
                {
                    var message = JsonSerializer.Deserialize<Message>(rawMessage);
                    if (message != null) Console.WriteLine($"Server: {message.Content}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("cant deserialize message from server");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error receiving message: {ex.Message}");
        }
        finally
        {
            _networkStream.Close();
            tcpClient.Close();
        }
    }
}