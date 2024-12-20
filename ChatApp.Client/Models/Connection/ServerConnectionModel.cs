using System.Net.Sockets;
using System.Text;

namespace ChatApp.Client.Wpf.Models.Connection;

public class ServerConnection
{
    private readonly NetworkStream _stream;

    public ServerConnection(TcpClient client)
    {
        _stream = client.GetStream();
    }
    
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