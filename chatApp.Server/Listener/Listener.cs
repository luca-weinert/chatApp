using System.Text.Json;
using System.Text.Json.Nodes;
using chatApp_server.Events;
using ChatApp.Shared.Connection;
using ChatApp.Shared.Events;

namespace chatApp_server.Listener
{
    public class Listener(IEventService eventService) : IListener
    {
        public event EventHandler<MessageEventArgs> MessageSend;
        
        public async Task ListenOnConnection(IConnection connection, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    // waiting for incoming data
                    var receivedData = await connection.ReadAsync(cancellationToken);
                    Console.WriteLine($"[Listener]: received data: {receivedData}");
                    if (string.IsNullOrEmpty(receivedData)) continue;

                    // read the current EventType e.g. MessageSendEvent, UserInformationReceivedEvent
                    var jsNode = JsonNode.Parse(receivedData);
                    var eventType = (EventType)jsNode?["EventType"]!.GetValue<int>()!;
                    
                    // deserialize the event based on the eventType
                    dynamic? eventData = eventType switch
                    {
                        EventType.UserInformationResponse =>
                            JsonSerializer.Deserialize<Event<ChatApp.Shared.User.User>>(receivedData),
                        EventType.SendMessage => JsonSerializer.Deserialize<Event<ChatApp.Shared.Message.Message>>(
                            receivedData),
                        _ => JsonSerializer.Deserialize<Event<object>>(receivedData)
                    };

                    if (eventData == null) continue;
                    Console.WriteLine($"[Listener] related event: {eventData}");
                    
                    // handle the event
                    _ = Task.Run(() => eventService.HandleEventAsync(eventData), cancellationToken);
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

        private void OnMessageSend(MessageEventArgs e)
        {
            MessageSend?.Invoke(this, e);
        }
    }
}