using ChatApp.Communication;
using ChatApp.Shared.User;

namespace ChatApp.Client.Wpf.Communication;

public interface ICommunicationService
{
    public Event<Shared.Message.Message> CreateSendMessageEvent(Shared.Message.Message message);
    public Event<Shared.User.User> CreateSendUserInformationEvent (Shared.User.User user);
    
    public Task SendEventToServerAsync<T>(Event<T> eventToSend);
}