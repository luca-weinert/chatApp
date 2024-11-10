﻿using System.Text.Json;
using ChatApp.Communication.Events;
using ChatApp.Shared.Connection;
using ChatApp.Shared.Events;
using ChatApp.Shared.Message;

namespace ChatApp.Communication.Listener
{
    public class Listener(IEventService eventService) : IListener
    {
        public async Task ListenOnConnection(IConnection connection, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var receivedData = await connection.ReadAsync(cancellationToken);
                    
                    Console.WriteLine($"[Listener]: received data: {receivedData}");
                    
                    if (string.IsNullOrEmpty(receivedData)) continue;
                    var eventData = JsonSerializer.Deserialize<Event<object>>(receivedData);
                    if (eventData == null) continue;

                    if (eventData.EventType == EventType.SendMessage)
                    {
                        var msg = (Message)eventData.Payload;
                        var serialiedMessag  = JsonSerializer.Serialize(msg);
                        Console.WriteLine($"[Listener] SerializedMessage: {serialiedMessag}");
                    }
                    
                    Console.WriteLine($"[Listener] Received event: {eventData}");
                    _ = Task.Run(() => eventService.HandleEventAsync(eventData, connection), cancellationToken);
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

