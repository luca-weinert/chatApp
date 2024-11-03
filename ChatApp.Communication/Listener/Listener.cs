using System.Text.Json;
using ChatApp.Communication.Event;
using ChatApp.Shared.Connection;


namespace ChatApp.Shared.Listener
{
    public class Listener : IListener
    {
        private readonly IEventHandler _eventHandler;

        public Listener(IEventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }
    
        public async Task ListenOnConnection(IConnection connection, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var receivedData = await connection.ReadAsync(cancellationToken);
                    if (string.IsNullOrEmpty(receivedData)) continue;
                    Console.WriteLine(receivedData);
                    var eventData = JsonSerializer.Deserialize<Event<object>>(receivedData);
                    if (eventData != null)
                    {
                        await _eventHandler.HandleEventAsync(eventData, connection);
                    }
                }
            }
            catch (JsonException e)
            {
                Console.WriteLine(e);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

