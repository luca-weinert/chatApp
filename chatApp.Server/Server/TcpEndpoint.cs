using System.Net;
using System.Net.Sockets;
using System.Text;
using chatApp_server.Connection;
using chatApp_server.user;

namespace chatApp_server.Server;

public class TcpEndpoint
{
    private readonly TcpListener _tcpListener = new(IPAddress.Parse("192.168.21.2"), 8080);
    private readonly UserService _userService;
    private readonly ConnectionService _connectionService;
    private Task _listenerTask;

    public TcpEndpoint(UserService userService, ConnectionService connectionService)
    {
        _userService = userService;
        _connectionService = connectionService;
    }

    public async Task StartAsync()
    {
        _tcpListener.Start();

        while (true)
        {
            Console.WriteLine("Waiting for connection...");
            var client = await _tcpListener.AcceptTcpClientAsync();
            Console.WriteLine("Client connected");
            var stream = client.GetStream();
            var buffer = new byte[256];
            var bytesRead = await stream.ReadAsync(buffer);
            var text = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received: " + text);
            // Handle client communication, process `bytesRead`
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _tcpListener.Stop();
        return _listenerTask ?? Task.CompletedTask; // Ensure that the task completes.
    }
}