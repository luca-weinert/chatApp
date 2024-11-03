using System.Net.Sockets;

namespace ChatApp.Shared;

public class Connection
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