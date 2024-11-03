using ChatApp.Communication;

namespace ChatApp.Client.Wpf.Event;

public class CustomEventHandler : IEventHandler
{
    public Task HandleEventAsync<T>(Event<T> eEvent)
    {
        throw new NotImplementedException();
    }
}