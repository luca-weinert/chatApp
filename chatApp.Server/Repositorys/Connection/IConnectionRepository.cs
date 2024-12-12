using ChatApp.Shared.Model.Connection;

namespace chatApp_server.Repositorys.Connection;

public interface IConnectionRepository
{
    public Task SaveConnectionAsync(IConnection clientConnection);
    public Task RemoveConnectionAsync(IConnection clientConnection);
    public Task<IConnection?> GetConnectionByUserIdAsync(Guid userId);
    public IConnection UpdateConnection(IConnection connection);
}