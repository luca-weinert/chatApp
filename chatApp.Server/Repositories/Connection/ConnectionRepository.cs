using ChatApp.Server.Models.Connection;

namespace ChatApp.Server.Repositories.Connection;

public class ConnectionRepository
{
    private ClientConnectionPool _clientConnectionPool;

    public ConnectionRepository()
    {
        _clientConnectionPool = ClientConnectionPool.Instance;
    }
    
    public  Task AddConnectionAsync(ClientConnection clientConnection)
    {
        _clientConnectionPool.connectedCliens.TryAdd(clientConnection.Id, clientConnection);
        return Task.CompletedTask;
    }

    public Task RemoveConnectionAsync(ClientConnection clientConnection)
    {
        _clientConnectionPool.connectedCliens.TryRemove(clientConnection.Id, out _);
        return Task.CompletedTask;
    }

    public  Task<ClientConnection?> GetConnectionByUserIdAsync(Guid userId)
    {
        var connection = _clientConnectionPool.connectedCliens.Values.FirstOrDefault(c => c.UserId == userId);
        return Task.FromResult(connection);
    }    
    public  Task<ClientConnection?> GetConnectionByConnectionId(Guid connectionId)
    {
        var connection = _clientConnectionPool.connectedCliens.Values.FirstOrDefault(c => c.Id == connectionId);
        return Task.FromResult(connection);
    }

    public ClientConnection UpdateConnection(ClientConnection connection)
    {
        var updatedConnection =
            _clientConnectionPool.connectedCliens.AddOrUpdate(connection.Id, connection, (key, oldValue) => connection);
        return updatedConnection;
    }
}