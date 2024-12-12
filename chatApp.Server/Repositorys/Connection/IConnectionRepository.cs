using ChatApp.Shared.Connection;

namespace chatApp_server.Connection.Repository;

public interface IConnectionRepository
{
    public Task SaveConnectionAsync(IConnection clientConnection);
    public Task RemoveConnectionAsync(IConnection clientConnection);
    public Task<IConnection?> GetConnectionByUserIdAsync(Guid userId);
    public IConnection UpdateConnection(IConnection connection);
}