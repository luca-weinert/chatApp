using System.Net.Sockets;

namespace ChatApp.Shared.Connection;

public class Connection : IConnection
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
}