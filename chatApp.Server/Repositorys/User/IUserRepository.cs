namespace chatApp_server.Repositorys.User;

public interface IUserRepository
{
    public Task SaveUserAsync(ChatApp.Shared.Model.User.User user); 
    public Task<ChatApp.Shared.Model.User.User?> GetUserByIdAsync(Guid userId);
    public Task RemoveUserByIdAsync(Guid userId);
}