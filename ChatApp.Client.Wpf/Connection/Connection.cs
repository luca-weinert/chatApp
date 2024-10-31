using System.IO;
using System.Net.Sockets;

namespace ChatApp.Client.Wpf.Connection;

public class Connection(TcpClient tcpClient, NetworkStream networkStream)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public TcpClient Client { get; set; } = tcpClient;
    public Stream NetworkStrean { get; set; } = networkStream;
}