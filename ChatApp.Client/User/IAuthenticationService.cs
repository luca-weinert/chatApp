namespace ChatApp.Client.Wpf.User;

public interface IAuthenticationService
{
    public Task AuthenticateUserAsync(Shared.User.User user);
}