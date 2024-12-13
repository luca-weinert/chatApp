using ChatApp.Server.Models.Connection;

namespace ChatApp.Server.Repositorys.Connection;

public class ConnectionRepository
{
    private ConnectionPool _connectionPool;

    public ConnectionRepository()
    {
        _connectionPool = ConnectionPool.GetInstance();
    }


    public Task AddConnectionAsync(ClientConnection clientConnection)
    {
        _connectionPool.connectedCliens.TryAdd(clientConnection.Id, clientConnection);
        return Task.CompletedTask;
    }

    public Task RemoveConnectionAsync(ClientConnection clientConnection)
    {
        _connectionPool.connectedCliens.TryRemove(clientConnection.Id, out _);
        return Task.CompletedTask;
    }

    public Task<ClientConnection?> GetConnectionByUserIdAsync(Guid userId)
    {
        var connection = _connectionPool.connectedCliens.Values.FirstOrDefault(c => c.UserId == userId);
        return Task.FromResult(connection);
    }

    public ClientConnection UpdateConnection(ClientConnection connection)
    {
        var updatedConnection =
            _connectionPool.connectedCliens.AddOrUpdate(connection.Id, connection, (key, oldValue) => connection);
        return updatedConnection;
    }
}