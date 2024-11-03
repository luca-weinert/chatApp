using System.Net.Sockets;
using System.Text;

namespace ChatApp.Shared.Connection;

public class Connection : IConnection, IDisposable
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

    public async Task<string> ReadAsync()
    {
        var buffer = new byte[1_024];
        var messageBuilder = new StringBuilder();

        while (true)
        {
            var receivedBytes = await NetworkStream.ReadAsync(buffer);
            if (receivedBytes == 0)
            {
                break;
            }

            var jsonString = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
            messageBuilder.Append(jsonString);
        }

        return messageBuilder.ToString();
    }

    public async Task<bool> WriteAsync(string data)
    {
        try
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            await NetworkStream.WriteAsync(bytes);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"something went wrong while writing: {ex}");
            return false;
        }
    }

    public void Dispose()
    {
        NetworkStream?.Dispose();
        Client?.Close();
        Client?.Dispose();
    }
}