using ChatApp.ChatProtocol.Models;
using ChatApp.Shared.Model.User;

namespace ChatApp.Client.Wpf.Services.AuthenticationService;

public class AuthenticationService
{
    private readonly ChatProtocolService.ChatProtocolService _chatProtocolService;
    private readonly User _user;

    private AuthenticationService()
    {
        _chatProtocolService = new ChatProtocolService.ChatProtocolService();
        _user = new User("Luca Weinert");
    }

    private static AuthenticationService? _instance;
    private static readonly object _lock = new();

    public static AuthenticationService Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new AuthenticationService();
                }

                return _instance;
            }
        }
    }

    public async Task AuthenticateUserAsync()
    {
        var chatProtocolDataPackage = new ChatProtocolDataPackage(ChatProtocolPayloadTypes.User, _user.ToJson());
        await _chatProtocolService.SendAsync(chatProtocolDataPackage);
    }

    public User GetUser() => _user;
}