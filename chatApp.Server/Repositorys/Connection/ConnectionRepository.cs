using System.Collections.Concurrent;
using ChatApp.Server.Models.Connection;
using ChatApp.Shared.Model.Connection;

namespace ChatApp.Server.Repositorys.Connection;

public class ConnectionRepository : IConnectionRepository
{
    public ConnectionRepository()
    {
    }
    
    private readonly ConcurrentDictionary<Guid, IClientConnection> _connectionPool = new ConcurrentDictionary<Guid, IClientConnection>();
    
    public Task SaveConnectionAsync(IClientConnection clientConnection)
    {
        _connectionPool.TryAdd(clientConnection.Id, clientConnection);
        return Task.CompletedTask;
    }
    
    public Task RemoveConnectionAsync(IClientConnection clientConnection)
    {
        _connectionPool.TryRemove(clientConnection.Id, out _);
        return Task.CompletedTask;
    }

    public Task<IClientConnection?> GetConnectionByUserIdAsync(Guid userId)
    {
        var connection = _connectionPool.Values.FirstOrDefault(c => c.UserId == userId);
        return Task.FromResult(connection);
    }

    public IClientConnection UpdateConnection(IClientConnection connection)
    {
       var updatedConnection =  _connectionPool.AddOrUpdate(connection.Id, connection, (key, oldValue) => connection);
       return updatedConnection;
    }
}