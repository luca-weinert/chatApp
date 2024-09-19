using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace chatApp_server;

public class TcpServer
{
    private readonly TcpListener _tcpListener = new(IPAddress.Parse("192.168.8.61"), 8080);
    public List<ConnectedClient> ConnectedClients = [];
    
    public async Task ListenAsync()
    {
        _tcpListener.Start();

        try
        {
            while (true)
            {
                Console.WriteLine("Server is running and ready for connections...");
                var client = await _tcpListener.AcceptTcpClientAsync();
                var connectedClient = new ConnectedClient(client);
                ConnectedClients.Add(connectedClient);
                
                _ = Task.Run(() => HandleClientAsync(connectedClient));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            _tcpListener.Stop();
        }
    }

    private async Task HandleClientAsync(ConnectedClient connectedClient)
    {
        var buffer = new byte[1024];
        var stream = connectedClient.Stream;

        try
        {
            while (true)
            {
                var bytesRead = await stream.ReadAsync(buffer);

                if (bytesRead == 0)
                {
                    // Connection closed by the client
                    break;
                }

                // Handle the message (e.g., broadcast or process message)
                var rawMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                var message = JsonSerializer.Deserialize<Message>(rawMessage);
                
                Console.WriteLine($"Received message: {rawMessage}");

                // Example: echo the message back to the client
                await SentMessage(connectedClient, $"server recieved following message: {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Client has disconnected, clean up
            Console.WriteLine("Client disconnected.");
            ConnectedClients.Remove(connectedClient);
            connectedClient.Dispose();
        }
    }

    public async Task SentMessage(ConnectedClient connectedClient, string message)
    {
        if (connectedClient.Stream.CanWrite)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            await connectedClient.Stream.WriteAsync(messageBytes, 0, messageBytes.Length);
        }
    }
}