namespace ChatApp.Client.Wpf.User;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationService()
    {
    }
    
    public async Task AuthenticateUserAsync()
    {
    }

    public Task<Shared.User.User> GetUserInformationAsync()
    {
        var testUSer = new Shared.User.User("Luca Weinert");
        return Task.FromResult<Shared.User.User>(testUSer);
    }
}