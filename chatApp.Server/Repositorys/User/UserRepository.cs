using System.Collections.Concurrent;

namespace chatApp_server.User;

public class UserRepository : IUserRepository
{
    private ConcurrentDictionary<Guid,ChatApp.Shared.User.User> _users = new();
    
    public Task SaveUserAsync(ChatApp.Shared.User.User user)
    {
        _users.TryAdd(user.Id, user);
        return Task.CompletedTask;
    }

    public Task<ChatApp.Shared.User.User?> GetUserByIdAsync(Guid userId)
    {
        _users.TryGetValue(userId, out var user);
        return Task.FromResult(user);
    }

    public Task RemoveUserByIdAsync(Guid userId)
    {
        _users.TryRemove(userId, out var user);
        return Task.CompletedTask;
    }
}