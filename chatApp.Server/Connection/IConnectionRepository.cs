namespace chatApp_server.Connection;

public interface IConnectionRepository
{
    public Task SaveConnectionAsync(ChatApp.Shared.Connection.Connection clientConnection);
    public Task RemoveConnectionAsync(ChatApp.Shared.Connection.Connection clientConnection);
    public Task<ChatApp.Shared.Connection.Connection?> GetConnectionByUserId(Guid userId);
}