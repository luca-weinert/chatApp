using ChatApp.Shared.Events;

namespace ChatApp.Client.Wpf.Event;

public class EventFactory : IEventFactory
{
    public Event<Shared.Message.Message> CreateSendMessageEvent(Shared.Message.Message message)
    {
        return new Event<Shared.Message.Message>(EventType.SendMessage, message);
    }

    public Event<Shared.User.User> CreateUserInformationResponseEvent(Shared.User.User user)
    {
        return new Event<Shared.User.User>(EventType.UserInformation, user);
    }

    public Event<object?> CreateUserInformationRequestEvent(Shared.Message.Message message)
    {
        return new Event<object?>(EventType.UserInformationRequest, null);
    }
}