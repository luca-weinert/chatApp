using ChatApp.Shared.Events;

namespace chatApp_server.Events;

public interface IEventFactory
{
    public Event<ChatApp.Shared.Message.Message> CreateSendMessageEvent(ChatApp.Shared.Message.Message message);
    public Event<ChatApp.Shared.User.User> CreateUserInformationResponseEvent(ChatApp.Shared.User.User user);
    public Event<object?> CreateUserInformationRequestEvent(ChatApp.Shared.Message.Message message);
}