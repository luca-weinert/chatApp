using System.Net.Sockets;

namespace ChatApp.Client.Wpf;

public class TcpConnector
{
    public TcpConnector()
    {
    }

    public async Task<Connection> GetConnectionToServer()
    {
        try
        {
            var tcpClient = new TcpClient("172.22.224.1", 8080);
            var stream = tcpClient.GetStream();
            return new Connection(tcpClient, stream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}