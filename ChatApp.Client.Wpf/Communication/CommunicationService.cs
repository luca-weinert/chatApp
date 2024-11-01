using ChatApp.Client.Wpf.Connection;
using ChatApp.Communication;

namespace ChatApp.Client.Wpf.Communication;

public class CommunicationService : ICommunicationService
{
    private readonly ServerConnection _serverConnection;
    private readonly IEventSender _eventSender;

    public CommunicationService(ServerConnection serverConnection, IEventSender eventSender)
    {
        _serverConnection = serverConnection;
        _eventSender = eventSender;
    }
    
    public async Task SendEventToServerAsync<T>(Event<T> eventToSend)
    {
        await _eventSender.SendEventAsync(eventToSend, _serverConnection.NetworkStream);
    }
}