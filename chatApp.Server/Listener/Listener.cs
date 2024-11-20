using System.Text.Json;
using System.Text.Json.Nodes;
using chatApp_server.Events;
using ChatApp.Shared.Connection;
using ChatApp.Shared.Events;
using ChatApp.SuperProtocol;

namespace chatApp_server.Listener
{
    public class Listener(IEventService eventService) : IListener
    {
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

                    var chatData = JsonSerializer.Deserialize<ChatAppDataPackage>(receivedData);

                    switch (chatData.DataType)
                    {
                        case ChatAppDataTypes.Message:
                            Console.WriteLine($"message!");
                            if (!string.IsNullOrWhiteSpace(chatData.Data))
                            {
                                var message = JsonSerializer.Deserialize<ChatApp.Shared.Message.Message>(chatData.Data);
                                if (message != null)
                                {
                                    Console.WriteLine($"message: {message}");
                                }
                            }
                            break;
                        case ChatAppDataTypes.User:
                            Console.WriteLine($"user!");
                            if (!string.IsNullOrWhiteSpace(chatData.Data))
                            {
                                var user = JsonSerializer.Deserialize<ChatApp.Shared.User.User>(chatData.Data);
                                if (user != null)
                                {
                                    Console.WriteLine($"user: {user}");
                                }
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("chatData.DataType not supported by the server");
                            break;
                    }
                    
                    /*// read the current EventType e.g. MessageSendEvent, UserInformationReceivedEvent
                    var jsNode = JsonNode.Parse(receivedData);
                    var eventType = (EventType)jsNode?["EventType"]!.GetValue<int>()!;
                    
                    // deserialize the event based on the eventType
                    dynamic? eventData = eventType switch
                    {
                        EventType.UserInformation =>
                            JsonSerializer.Deserialize<Event<ChatApp.Shared.User.User>>(receivedData),
                        EventType.SendMessage => JsonSerializer.Deserialize<Event<ChatApp.Shared.Message.Message>>(
                            receivedData),
                        _ => JsonSerializer.Deserialize<Event<object>>(receivedData)
                    };

                    if (eventData == null) continue;
                    Console.WriteLine($"[Listener] related event: {eventData}");
                    
                    _ = Task.Run(() => eventService.HandleEventAsync(eventData), cancellationToken);*/
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