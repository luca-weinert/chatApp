using System.Net.Sockets;

namespace chatApp_server.Connection;

public class ConnectionService : IConnectionService
{
    private readonly IConnectionRepository _connectionRepository;

    public ConnectionService(IConnectionRepository connectionRepository)
    {
        _connectionRepository = connectionRepository;
    }

    public async Task<ChatApp.Shared.Connection> GetConnectionForClientAsync(TcpClient tcpClient)
    {
        return new ChatApp.Shared.Connection(tcpClient);
    }
}