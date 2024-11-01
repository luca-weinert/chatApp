namespace chatApp_server.Connection;

public interface IConnectionRepository
{
    public Task SaveConnectionAsync(ClientConnection clientConnection);
    public Task RemoveConnectionAsync(ClientConnection clientConnection);
    public Task<ClientConnection?> GetConnectionByUserId(Guid userId);
}