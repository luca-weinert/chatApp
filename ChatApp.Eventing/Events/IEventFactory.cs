using ChatApp.Shared.Message;
using ChatApp.Shared.User;

namespace ChatApp.Communication.Events;

public interface IEventFactory
{
    public Event<Message> CreateSendMessageEvent(Message message);
    public Event<User> CreateUserInformationResponseEvent(User user);
    public Event<object?> CreateUserInformationRequestEvent(Message message);
}