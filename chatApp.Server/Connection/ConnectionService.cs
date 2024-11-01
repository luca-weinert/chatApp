using System.Net.Sockets;

namespace chatApp_server.Connection;

public class ConnectionService : IConnectionService
{
    private readonly IConnectionRepository _connectionRepository;

    public ConnectionService(IConnectionRepository connectionRepository)
    {
        _connectionRepository = connectionRepository;
    }

    public async Task<ClientConnection> GetConnectionForClientAsync(TcpClient tcpClient)
    {
        var clientConnection = new ClientConnection(tcpClient);
        return clientConnection;
    }
}