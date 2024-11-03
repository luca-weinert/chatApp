namespace ChatApp.Communication;

public interface IEventTransmitter
{
    public Task SendEventAsync<T>(Event<T> eventToSend, Stream targetStream);
}