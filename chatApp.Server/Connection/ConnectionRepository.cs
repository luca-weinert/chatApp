using System.Collections.Concurrent;

namespace chatApp_server.Connection;

public class ConnectionRepository : IConnectionRepository
{

    public ConnectionRepository()
    {
    }
    
    private ConcurrentDictionary<Guid, Connection> _connections = new ConcurrentDictionary<Guid, Connection>();
    
    public Task SaveConnectionAsync(Connection connection)
    {
        _connections.TryAdd(connection.Id, connection);
        return Task.CompletedTask;
    }

    public Task RemoveConnectionAsync(Connection connection)
    {
        _connections.TryRemove(connection.Id, out _);
        return Task.CompletedTask;
    }

    public Task<Connection?> GetConnectionByUserId(Guid userId)
    {
        var connection = _connections.Values.FirstOrDefault(c => c.RelatedUserId == userId);
        return Task.FromResult(connection);
    }
}