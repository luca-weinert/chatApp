using ChatApp.Shared;

namespace chatApp_server.user;

public interface IUserRepository
{
    public Task SaveUserAsync(User user); 
    public Task<User?> GetUserByIdAsync(Guid userId);
    public Task RemoveUserByIdAsync(Guid userId);
}