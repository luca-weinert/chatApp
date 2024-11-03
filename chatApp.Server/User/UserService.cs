using ChatApp.Communication.Event;
using ChatApp.Shared.Connection;

namespace chatApp_server.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEventFactory _eventFactory;
    
    public UserService(IUserRepository userRepository, IEventFactory eventFactory)
    {
        _userRepository = userRepository;
        _eventFactory  = eventFactory;
    }

    public async Task RequestUserInformationForAsync(IConnection clientConnection)
    {
    }
    
    public async Task HandleUserInformationAsync(IConnection clientConnection, ChatApp.Shared.User.User user)
    {
        // var msg = new ChatApp.Shared.Message.Message();
        // var msgEvent = _eventFactory.CreateSendMessageEvent(msg);
        // await _userRepository.SaveUserAsync(user);
        // Console.WriteLine($"[Server]: User information for {user.Name} saved successfully.");
    }
}