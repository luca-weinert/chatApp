using System.Collections.Concurrent;
using ChatApp.Shared.Model.Connection;

namespace chatApp_server.Repositorys.Connection;

public class ConnectionRepository : IConnectionRepository
{
    public ConnectionRepository()
    {
    }
    
    private readonly ConcurrentDictionary<Guid, IConnection> _connectionPool = new ConcurrentDictionary<Guid, IConnection>();
    
    public Task SaveConnectionAsync(IConnection clientConnection)
    {
        _connectionPool.TryAdd(clientConnection.Id, clientConnection);
        return Task.CompletedTask;
    }

    public Task RemoveConnectionAsync(IConnection clientConnection)
    {
        _connectionPool.TryRemove(clientConnection.Id, out _);
        return Task.CompletedTask;
    }

    public Task<IConnection?> GetConnectionByUserIdAsync(Guid userId)
    {
        var connection = _connectionPool.Values.FirstOrDefault(c => c.UserId == userId);
        return Task.FromResult(connection);
    }

    public IConnection UpdateConnection(IConnection connection)
    {
       var updatedConnection =  _connectionPool.AddOrUpdate(connection.Id, connection, (key, oldValue) => connection);
       return updatedConnection;
    }
}