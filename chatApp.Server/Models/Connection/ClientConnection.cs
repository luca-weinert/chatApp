using System.Net.Sockets;
using System.Text;

namespace ChatApp.Server.Models.Connection;

public class ClientConnection
{
    private readonly NetworkStream _stream;

    public ClientConnection(TcpClient client)
    {
        _stream = client.GetStream();
    }
    
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get;  set; }
    
    public async Task WriteAsync(string rawData)
    {
        var bytes = Encoding.ASCII.GetBytes(rawData);
        await _stream.WriteAsync(bytes);
    }

    public async Task<string> ReadAsync()
    {
        var buffer = new byte[1024];
        var receivedBytes = await _stream.ReadAsync(buffer);
        var result = Encoding.ASCII.GetString(buffer, 0, receivedBytes);
        return result;
    }
}