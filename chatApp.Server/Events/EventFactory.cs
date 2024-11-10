using ChatApp.Shared.Events;

namespace chatApp_server.Events;

public class EventFactory : IEventFactory
{
    public Event<ChatApp.Shared.Message.Message> CreateSendMessageEvent(ChatApp.Shared.Message.Message message)
    {
        return new Event<ChatApp.Shared.Message.Message>(EventType.SendMessage, message);
    }

    public Event<ChatApp.Shared.User.User> CreateUserInformationResponseEvent(ChatApp.Shared.User.User user)
    {
        return new Event<ChatApp.Shared.User.User>(EventType.UserInformationResponse, user);
    }

    public Event<object?> CreateUserInformationRequestEvent(ChatApp.Shared.Message.Message message)
    {
        return new Event<object?>(EventType.UserInformationRequest, null);
    }
}