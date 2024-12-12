namespace chatApp_server.User;

public interface IUserRepository
{
    public Task SaveUserAsync(ChatApp.Shared.User.User user); 
    public Task<ChatApp.Shared.User.User?> GetUserByIdAsync(Guid userId);
    public Task RemoveUserByIdAsync(Guid userId);
}