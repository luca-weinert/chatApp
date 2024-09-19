using System.Net.Sockets;

namespace chatApp_server;

public class Connection(TcpClient client) 
{
    private TcpClient Client { get; } = client;
    public NetworkStream Stream { get; } = client.GetStream();

    public void CLoseConnection()
    {
        Stream.Close();
        Client.Close();
    }
}