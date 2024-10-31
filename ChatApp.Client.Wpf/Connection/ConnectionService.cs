using System.Net;
using System.Net.Sockets;

namespace ChatApp.Client.Wpf.Connection;

public class ConnectionService : IConnectionService
{
    public async Task<Connection> ConnectToAsync(IPEndPoint endpoint)
    {
        Console.WriteLine("trying to connect to endpoint...");

        try
        {
            var tcpClient = new TcpClient(endpoint.Address.ToString(), endpoint.Port);
            var stream = tcpClient.GetStream();
            
            Console.WriteLine("Connected to server.");
            
            return new Connection(tcpClient, stream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}