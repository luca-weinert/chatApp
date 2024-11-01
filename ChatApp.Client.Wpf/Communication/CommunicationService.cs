using System.Text;
using System.Text.Json;
using ChatApp.Client.Wpf.Connection;
using ChatApp.Communication;
using ChatApp.Shared.User;

namespace ChatApp.Client.Wpf.Communication;

public class CommunicationService : ICommunicationService
{
    private readonly ServerConnection _serverConnection;


    public CommunicationService(ServerConnection serverConnection)
    {
        _serverConnection = serverConnection;
    }
    
    public Event<Shared.Message.Message> CreateSendMessageEvent(Shared.Message.Message message)
    {
        var sendMessageEvent = new Event<Shared.Message.Message>(EventType.SendMessage, message);
        return sendMessageEvent;
    }

    public Event<Shared.User.User> CreateSendUserInformationEvent(Shared.User.User user)
    {
        var sendUserInformationEvent = new Event<Shared.User.User>(EventType.SendUserInformation, user);
        return sendUserInformationEvent;
    }
    
    public async Task SendEventToServerAsync<T>(Event<T> eventToSend)
    {
        var serializedEvent = JsonSerializer.Serialize(eventToSend);
        var bytes = Encoding.UTF8.GetBytes(serializedEvent);
        await _serverConnection.NetworkStream.WriteAsync(bytes);
        Console.WriteLine($"Sent event: {eventToSend.EventType} payload: {eventToSend.Payload}");
    }
}