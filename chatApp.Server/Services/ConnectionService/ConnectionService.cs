using System.Net.Sockets;
using ChatApp.Server.Models.Connection;
using ChatApp.Server.Repositories.Connection;

namespace ChatApp.Server.Services.ConnectionService;

public class ConnectionService
{
    private readonly ConnectionRepository _connectionRepository;

    public ConnectionService()
    {
        _connectionRepository = new ConnectionRepository();
    }

    public ClientConnection GetConnectionForClient(TcpClient tcpClient)
    {
        var connection = new ClientConnection(tcpClient);
        return connection;
    }

    public ClientConnection UpdateConnection(ClientConnection connection)
    {
       var updatedConnection =  _connectionRepository.UpdateConnection(connection);
       return updatedConnection;
    }
    
    public async Task<ClientConnection> GetConnectionByUserIdAsync(Guid userId)
    {
        var connection = await _connectionRepository.GetConnectionByUserIdAsync(userId);
        return connection;
    }

    public Task<bool> isUserConnected(Guid userId)
    {
        var connection = _connectionRepository.GetConnectionByUserIdAsync(userId);
        return Task.FromResult(connection != null);
    }
}