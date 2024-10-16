using System.Net;
using System.Net.Sockets;
using chatApp_server.Connection;
using chatApp_server.user;

namespace chatApp_server.Server;

public class TcpEndpoint(IUserService userService, IConnectionService connectionService)
{
    private readonly TcpListener _tcpListener = new(IPAddress.Parse("172.22.224.1"), 8080);

    public async Task StartAsync()
    {
        _tcpListener.Start();

        while (true)
        {
            Console.WriteLine("Waiting for connection...");
            var client = await _tcpListener.AcceptTcpClientAsync();
            Console.WriteLine("Client connected");
            await HandleClient(client);
        }
    }

    private async Task HandleClient(TcpClient client)
    {
        var connection = new Connection.Connection(client);
        var user = await userService.GetUserInformation(connection);
        connection.RelatedUserId = user.Id;
        await connectionService.AddConnection(connection);
        Console.WriteLine($"user received: id: {user.Id}, name: {user.Name}");
    }

    public void StopAsync(CancellationToken cancellationToken)
    {
        _tcpListener.Stop();
    }
}