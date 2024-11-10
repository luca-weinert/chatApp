using ChatApp.Shared.Events;

namespace ChatApp.Client.Wpf.Event;

public interface IEventFactory
{
    public Event<Shared.Message.Message> CreateSendMessageEvent(Shared.Message.Message message);
    public Event<Shared.User.User> CreateUserInformationResponseEvent(Shared.User.User user);
    public Event<object?> CreateUserInformationRequestEvent(Shared.Message.Message message);
}