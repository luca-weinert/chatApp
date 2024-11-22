using System.Net.Sockets;
using System.Text;
using ChatApp.SuperProtocol;

namespace ChatApp.Client.Wpf.Connection;

public class ServerConnection : IServerConnection
{
    private NetworkStream _stream;

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

    public Task<string> ReadAsync()
    {
        throw new NotImplementedException();
    }
}