using ChatApp.Communication;
using ChatApp.Shared.Connection;

namespace ChatApp.Client.Wpf.Communication;

public class Listener : IListener
{
    private readonly IEventHandler _eventHandler;

    public Listener(IEventHandler eventHandler)
    {
        _eventHandler = eventHandler;
    }
    
    public async Task ListenOnConnection(IConnection connection, CancellationToken cancellationToken)
    {
        Console.WriteLine("[Client] Listener is running.");
        var receivedData = await connection.ReadAsync();
        Console.WriteLine($"[Client]: Received data: {receivedData}");
    }
}