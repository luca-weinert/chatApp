using ChatApp.Server.Models.User;

namespace ChatApp.Server.Repositories.User;

public class UserRepository
{
   private UserPool _userPool;

   public UserRepository()
   {
       _userPool = UserPool.Instance;
   }
    
    public Task SaveUserAsync(ChatApp.Shared.Model.User.User user)
    {
        _userPool.users.TryAdd(user.Id, user);
        return Task.CompletedTask;
    }

    public Task<ChatApp.Shared.Model.User.User?> GetUserByIdAsync(Guid userId)
    {
        _userPool.users.TryGetValue(userId, out var user);
        return Task.FromResult(user);
    }

    public Task RemoveUserByIdAsync(Guid userId)
    {
        _userPool.users.TryRemove(userId, out var user);
        return Task.CompletedTask;
    }
}