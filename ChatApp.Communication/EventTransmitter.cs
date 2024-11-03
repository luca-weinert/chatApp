using System.Text;
using System.Text.Json;

namespace ChatApp.Communication;

public class EventTransmitter : IEventTransmitter
{
    public async Task SendEventAsync<T>(Event<T> eventToSend, Stream targetStream)
    {
        try
        {
            var serializedEvent = JsonSerializer.Serialize(eventToSend);
            var bytes = Encoding.UTF8.GetBytes(serializedEvent);
            await targetStream.WriteAsync(bytes);
            Console.WriteLine($"Sent event: {eventToSend.EventType} payload: {eventToSend.Payload}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"something went wrong while sending event: {ex}");
        }
    }
}