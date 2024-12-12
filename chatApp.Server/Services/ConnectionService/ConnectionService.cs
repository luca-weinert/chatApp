using System.Net.Sockets;
using ChatApp.Server.Models.Connection;
using ChatApp.Server.Repositorys.Connection;
using ChatApp.Shared.Model.Connection;

namespace ChatApp.Server.Services.ConnectionService;

public class ConnectionService : IConnectionService
{
    private readonly IConnectionRepository _connectionRepository;

    public ConnectionService(IConnectionRepository connectionRepository)
    {
        _connectionRepository = connectionRepository;
    }

    public IClientConnection GetConnectionForClient(TcpClient tcpClient)
    {
        var connection = new ClientConnection(tcpClient);
        return connection;
    }

    public  IClientConnection UpdateConnection(IClientConnection connection)
    {
       var updatedConnection =  _connectionRepository.UpdateConnection(connection);
       return updatedConnection;
    }
    
    public async Task<IClientConnection> GetConnectionByUserIdAsync(Guid userId)
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