using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace chatApp_server.Connection.Model;

public class ClientConnection
{
    private readonly NetworkStream _stream;

    public ClientConnection(TcpClient client)
    {
        _stream = client.GetStream();
    }
    
    public Guid Id { get; private set; } = Guid.NewGuid();
    
    public async Task WriteAsync(string rawMessage)
    {
        var serializedMessage = JsonSerializer.Serialize(rawMessage);
        var bytes = Encoding.UTF8.GetBytes(serializedMessage);
        await _stream.WriteAsync(bytes);
    }

    public async Task<string> ReadAsync()
    {
        var buffer = new byte[1024];
        var receivedBytes = await _stream.ReadAsync(buffer);
        var result = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
        return result;
    }
}