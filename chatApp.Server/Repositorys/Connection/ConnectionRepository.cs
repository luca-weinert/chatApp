using System.Collections.Concurrent;
using ChatApp.Server.Models.Connection;

namespace ChatApp.Server.Repositorys.Connection;

public class ConnectionRepository
{
    public ConnectionRepository()
    {
    }
    
    private readonly ConcurrentDictionary<Guid, ClientConnection> _connectionPool = new ConcurrentDictionary<Guid, ClientConnection>();
    
    public Task SaveConnectionAsync(ClientConnection clientConnection)
    {
        _connectionPool.TryAdd(clientConnection.Id, clientConnection);
        return Task.CompletedTask;
    }
    
    public Task RemoveConnectionAsync(ClientConnection clientConnection)
    {
        _connectionPool.TryRemove(clientConnection.Id, out _);
        return Task.CompletedTask;
    }

    public Task<ClientConnection?> GetConnectionByUserIdAsync(Guid userId)
    {
        var connection = _connectionPool.Values.FirstOrDefault(c => c.UserId == userId);
        return Task.FromResult(connection);
    }

    public ClientConnection UpdateConnection(ClientConnection connection)
    {
       var updatedConnection =  _connectionPool.AddOrUpdate(connection.Id, connection, (key, oldValue) => connection);
       return updatedConnection;
    }
}