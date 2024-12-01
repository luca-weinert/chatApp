using System.Net.Sockets;
using System.Text;
using ChatApp.SuperProtocol;

namespace ChatApp.Client.Wpf.Services.Connection;

public class ServerConnection : IServerConnection
{
    private readonly NetworkStream _stream;

    public ServerConnection(TcpClient client)
    {
        _stream = client.GetStream();
    }
    
    public async Task WriteAsync(SuperProtocolDataPackage package)
    {
        var serializedPackage = SuperProtocol.SuperProtocol.Serialize(package);
        var bytes = Encoding.UTF8.GetBytes(serializedPackage);
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