using System.Net.Sockets;

namespace chatApp_server.Connection;

public class ClientConnection(TcpClient client)
{
    public Guid Id = Guid.NewGuid(); 
    public byte[] buffer = new byte[1024];
    private TcpClient _client = client;
    public NetworkStream NetworkStream = client.GetStream();
    public Guid RelatedUserId { get; set; }
}