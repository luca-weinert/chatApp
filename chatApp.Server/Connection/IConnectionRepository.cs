namespace chatApp_server.Connection;

public interface IConnectionRepository
{
    public Task SaveConnectionAsync(Connection connection);
    public Task RemoveConnectionAsync(Connection connection);
    public Task<Connection?> GetConnectionByUserId(Guid userId);
}