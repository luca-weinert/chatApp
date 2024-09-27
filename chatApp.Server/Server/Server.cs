using System.Net;
using System.Net.Sockets;
using chatApp_server.Connection;
using chatApp_server.user;

namespace chatApp_server.Server;

public class Server(UserService userService, ConnectionService connectionService)
{
    private UserService _userService = userService;
    private ConnectionService _connectionService = connectionService;
    private readonly TcpListener _tcpListener = new(IPAddress.Parse("127.0.0.1"), 8080);

    public Task Start()
    {
        _connectionService.test();
        _tcpListener.Start();
        return Task.CompletedTask;
    }

    public void Stop()
    {
        _tcpListener.Stop();
    }
}