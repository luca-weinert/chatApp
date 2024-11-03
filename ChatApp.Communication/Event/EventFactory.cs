using ChatApp.Shared.Message;
using ChatApp.Shared.User;

namespace ChatApp.Communication;

public class EventFactory : IEventFactory
{
    public Event<Message> CreateSendMessageEvent(Message message)
    {
        return new Event<Message>(EventType.SendMessage, message);
    }

    public Event<User> CreateUserInformationResponseEvent(User user)
    {
        return new Event<User>(EventType.UserInformationResponse, user);
    }

    public Event<object?> CreateUserInformationRequestEvent(Message message)
    {
        return new Event<object?>(EventType.UserInformationRequest, null);
    }
}