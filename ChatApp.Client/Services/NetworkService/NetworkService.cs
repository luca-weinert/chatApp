using System.Net;
using System.Net.Sockets;
using ChatApp.Client.Wpf.Models.Connection;

namespace ChatApp.Client.Wpf.Services.NetworkService;

public class NetworkService
{
    private ServerConnection _serverConnection;
    private static NetworkService instance = null;
    private static readonly object padlock = new object();

    private NetworkService()
    {
    }

    public static NetworkService Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new NetworkService();
                }

                return instance;
            }
        }
    }

    public async Task ConnectAsync(IPEndPoint ipEndPoint)
    {
        Console.WriteLine("[Client]: Attempting to connect to the server...");

        try
        {
            var tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(ipEndPoint.Address, ipEndPoint.Port);

            Console.WriteLine("[Client]: Connection established successfully");
            _serverConnection = new ServerConnection(tcpClient);
        }
        catch (SocketException ex)
        {
            Console.WriteLine(
                $"[Client]: SocketException: Unable to connect to server at {ipEndPoint}. Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Client]: Unexpected error while connecting to server: {ex.Message}");
            throw;
        }
    }

    public async Task<string> ListenAsync()
    {
        if (_serverConnection == null) throw new Exception("no connection to server established");
        var receivedData = await _serverConnection.ReadAsync();
        return receivedData;
    }

    public async Task SendAsnAsync(string data)
    {
        if (_serverConnection == null) throw new Exception("no connection to server established");
        Console.WriteLine($"[Client]: Sending Data: {data}");
        await _serverConnection.WriteAsync(data);
    }
}