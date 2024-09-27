using System.Net.Sockets;

namespace chatApp_server.Connection;

public class Connection(TcpClient client)
{
    public Guid Id = Guid.NewGuid(); 
    private TcpClient _client = client;
    public NetworkStream Stream = client.GetStream();
    public Guid RelatedUserId { get; set; }
}