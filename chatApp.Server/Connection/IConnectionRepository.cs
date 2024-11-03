namespace chatApp_server.Connection;

public interface IConnectionRepository
{
    public Task SaveConnectionAsync(ChatApp.Shared.Connection clientConnection);
    public Task RemoveConnectionAsync(ChatApp.Shared.Connection clientConnection);
    public Task<ChatApp.Shared.Connection?> GetConnectionByUserId(Guid userId);
}