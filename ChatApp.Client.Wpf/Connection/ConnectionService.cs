using System.Net;
using System.Net.Sockets;

namespace ChatApp.Client.Wpf.Connection;

public class ConnectionService : IConnectionService
{
    public async Task<ServerConnection?> ConnectToServerAsync(IPEndPoint serverEndPoint)
    {
        Console.WriteLine("trying to connect to endpoint...");

        try
        {
            var tcpClient = new TcpClient(serverEndPoint.Address.ToString(), serverEndPoint.Port);
            var stream = tcpClient.GetStream();
            
            Console.WriteLine("Connected to server.");
            
            return new ServerConnection(tcpClient, stream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}