using ChatApp.Communication;

namespace ChatApp.Client.Wpf.Communication;

public class CommunicationService : ICommunicationService
{
    private readonly Shared.Connection _serverConnection;
    private readonly IEventTransmitter _eventTransmitter;

    public CommunicationService(Shared.Connection serverConnection, IEventTransmitter eventTransmitter)
    {
        _serverConnection = serverConnection;
        _eventTransmitter = eventTransmitter;
    }
    
    public async Task SendEventToServerAsync<T>(Event<T> eventToSend)
    {
        await _eventTransmitter.SendEventAsync(eventToSend, _serverConnection.NetworkStream);
    }
}