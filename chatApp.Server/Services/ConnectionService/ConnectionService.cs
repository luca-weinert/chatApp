using System.Net.Sockets;
using chatApp_server.Connection.Repository;
using ChatApp.Shared.Connection;

namespace chatApp_server.Connection.Services;

public class ConnectionService : IConnectionService
{
    private readonly IConnectionRepository _connectionRepository;

    public ConnectionService(IConnectionRepository connectionRepository)
    {
        _connectionRepository = connectionRepository;
    }

    public IConnection GetConnectionForClient(TcpClient tcpClient)
    {
        var connection = new ChatApp.Shared.Connection.Connection(tcpClient);
        return connection;
    }

    public  IConnection UpdateConnection(IConnection connection)
    {
       var updatedConnection =  _connectionRepository.UpdateConnection(connection);
       return updatedConnection;
    }
    
    public async Task<IConnection> GetConnectionByUserIdAsync(Guid userId)
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