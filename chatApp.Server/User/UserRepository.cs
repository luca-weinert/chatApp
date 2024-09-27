using System.Collections.Concurrent;
using ChatApp.Shared;

namespace chatApp_server.user;

public class UserRepository : IUserRepository
{
    private ConcurrentDictionary<Guid,User> _users = new();
    
    public Task SaveUserAsync(User user)
    {
        _users.TryAdd(user.Id, user);
        return Task.CompletedTask;
    }

    public Task<User?> GetUserByIdAsync(Guid userId)
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