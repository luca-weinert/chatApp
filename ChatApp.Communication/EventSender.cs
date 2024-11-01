using System.Text;
using System.Text.Json;

namespace ChatApp.Communication;

public class EventSender: IEventSender
{
    public async Task SendEventAsync<T>(Event<T> eventToSend, Stream targetStream)
    {
      var serializedEvent = JsonSerializer.Serialize(eventToSend);
      var bytes = Encoding.UTF8.GetBytes(serializedEvent);
      await targetStream.WriteAsync(bytes);
      Console.WriteLine($"Sent event: {eventToSend.EventType} payload: {eventToSend.Payload}");
    }
}