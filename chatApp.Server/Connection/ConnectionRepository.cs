using System.Collections.Concurrent;

namespace chatApp_server.Connection;

public class ConnectionRepository : IConnectionRepository
{

    public ConnectionRepository()
    {
    }
    
    private ConcurrentDictionary<Guid, ClientConnection> _connections = new ConcurrentDictionary<Guid, ClientConnection>();
    
    public Task SaveConnectionAsync(ClientConnection clientConnection)
    {
        _connections.TryAdd(clientConnection.Id, clientConnection);
        return Task.CompletedTask;
    }

    public Task RemoveConnectionAsync(ClientConnection clientConnection)
    {
        _connections.TryRemove(clientConnection.Id, out _);
        return Task.CompletedTask;
    }

    public Task<ClientConnection?> GetConnectionByUserId(Guid userId)
    {
        var connection = _connections.Values.FirstOrDefault(c => c.RelatedUserId == userId);
        return Task.FromResult(connection);
    }
}