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

    public async Task<string> ReadAsync(CancellationToken cancellationToken)
    {
        var buffer = new byte[1024];
        var messageBuilder = new StringBuilder();

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var receivedBytes = await NetworkStream.ReadAsync(buffer, cancellationToken);

                if (receivedBytes == 0)
                {
                    Console.WriteLine("[Connection]: The connection has been closed.");
                    break;
                }

                // Convert the received bytes to a UTF-8 string
                var jsonString = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                Console.WriteLine($"[Connection]: Data read: {jsonString}");
                messageBuilder.Append(jsonString);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Connection]: Error while reading data - {ex.Message}");
        }

        return messageBuilder.ToString();
    }


    public async Task<bool> WriteAsync(string data)
    {
        try
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            await NetworkStream.WriteAsync(bytes);
            Console.WriteLine("data written");
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