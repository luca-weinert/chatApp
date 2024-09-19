using System.Net.Sockets;

namespace chatApp_server;

public class ConnectedClient : IDisposable
{
    public TcpClient Client { get; }
    public NetworkStream Stream { get; }

    public ConnectedClient(TcpClient client)
    {
        Client = client;
        Stream = client.GetStream();
    }

    public void Dispose()
    {
        Stream.Close();
        Client.Close();
    }
}