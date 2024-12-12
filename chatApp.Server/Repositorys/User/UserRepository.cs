using System.Collections.Concurrent;

namespace chatApp_server.Repositorys.User;

public class UserRepository : IUserRepository
{
    private ConcurrentDictionary<Guid,ChatApp.Shared.Model.User.User> _users = new();
    
    public Task SaveUserAsync(ChatApp.Shared.Model.User.User user)
    {
        _users.TryAdd(user.Id, user);
        return Task.CompletedTask;
    }

    public Task<ChatApp.Shared.Model.User.User?> GetUserByIdAsync(Guid userId)
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