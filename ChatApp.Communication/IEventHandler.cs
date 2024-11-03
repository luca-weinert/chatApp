namespace ChatApp.Communication;

public interface IEventHandler
{
    public Task HandleEventAsync<T>(Event<T> eEvent);
}