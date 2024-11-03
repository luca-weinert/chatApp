using System.Collections.Concurrent;

namespace chatApp_server.Connection;

public class ConnectionRepository : IConnectionRepository
{

    public ConnectionRepository()
    {
    }
    
    private ConcurrentDictionary<Guid, ChatApp.Shared.Connection> _connections = new ConcurrentDictionary<Guid, ChatApp.Shared.Connection>();
    
    public Task SaveConnectionAsync(ChatApp.Shared.Connection clientConnection)
    {
        _connections.TryAdd(clientConnection.Id, clientConnection);
        return Task.CompletedTask;
    }

    public Task RemoveConnectionAsync(ChatApp.Shared.Connection clientConnection)
    {
        _connections.TryRemove(clientConnection.Id, out _);
        return Task.CompletedTask;
    }

    public Task<ChatApp.Shared.Connection?> GetConnectionByUserId(Guid userId)
    {
        var connection = _connections.Values.FirstOrDefault(c => c.UserId == userId);
        return Task.FromResult(connection);
    }
}