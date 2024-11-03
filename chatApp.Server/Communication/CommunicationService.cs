using System.Text;
using System.Text.Json;
using ChatApp.Communication;

namespace chatApp_server.Communication;

public class CommunicationService : ICommunicationService
{
    private readonly IEventHandler _eventHandler;

    public CommunicationService(IEventHandler eventHandler)
    {
        _eventHandler = eventHandler;
    }

    public async Task HandleCommunicationAsync(ChatApp.Shared.Connection clientConnection,
        CancellationToken cancellationToken)
    {
        try
        {
            var data = await ReadFromConnectionAsync(clientConnection, cancellationToken);
            var eventData = JsonSerializer.Deserialize<Event<object>>(data);
            if (eventData != null)
            {
                await _eventHandler.HandleEventAsync(eventData);
            }
        }
        catch (JsonException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<string> ReadFromConnectionAsync(ChatApp.Shared.Connection clientConnection,
        CancellationToken cancellationToken)
    {
        var buffer = new byte[1_024];
        var messageBuilder = new StringBuilder();

        while (!cancellationToken.IsCancellationRequested)
        {
            var receivedBytes = await clientConnection.NetworkStream.ReadAsync(buffer, cancellationToken);
            if (receivedBytes == 0)
            {
                // Connection closed gracefully
                break;
            }

            var jsonString = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
            Console.WriteLine($"[Server]: Data received: {jsonString}");
            messageBuilder.Append(jsonString);
        }

        return messageBuilder.ToString();
    }

    public async Task WriteOnConnectionAsync(ChatApp.Shared.Connection clientConnection, string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        await clientConnection.NetworkStream.WriteAsync(messageBytes);
        Console.WriteLine($"[Server]: Data sent: {message}");
    }
}
