using ChatApp.Server.Models.Connection;

namespace ChatApp.Server.Repositorys.Connection;

public interface IConnectionRepository
{
    public Task SaveConnectionAsync(IClientConnection clientConnection);
    public Task RemoveConnectionAsync(IClientConnection clientConnection);
    public Task<IClientConnection?> GetConnectionByUserIdAsync(Guid userId);
    public IClientConnection UpdateConnection(IClientConnection connection);
}