namespace ChatApp.Communication;

public interface IEventSender
{
    public Task SendEventAsync<T>(Event<T> eventToSend, Stream targetStream);
}