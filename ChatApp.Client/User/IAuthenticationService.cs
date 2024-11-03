namespace ChatApp.Client.Wpf.User;

public interface IAuthenticationService
{
    public Task AuthenticateUserAsync();
    public Task<Shared.User.User> GetUserInformationAsync();
}