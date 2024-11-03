using System.Net;
using System.Text;
using System.Text.Json;
using ChatApp.Client.Wpf.Connection;
using ChatApp.Communication;

namespace ChatApp.Client.Wpf.Communication;

public class CommunicationService : ICommunicationService
{
    private readonly IConnectionService _connectionService;
    private readonly IEventHandler _eventHandler;
    private Shared.Connection.Connection? _connection;

    public CommunicationService(IEventHandler eventHandler, IConnectionService connectionService)
    {
        _connectionService = connectionService;
        _eventHandler = eventHandler;
    }

    public async Task HandleCommunicationAsync()
    {
        if (await TryConnectToServerAsync())
        {
            var data = await ReadFromConnectionAsync();
            var eventData = JsonSerializer.Deserialize<Event<object>>(data);
            if (eventData != null)
            {
                Task.Run(() => _eventHandler.HandleEventAsync(eventData));
            }
            
        }
        else
        {
            Console.WriteLine("[Client]: Could not establish a connection to the server.");
        }
    }

    private async Task<bool> TryConnectToServerAsync()
    {
        try
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Parse("192.168.178.45"), 8080);
            _connection = await _connectionService.GetConnectionAsync(ipEndPoint);
            return _connection != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Client]: Failed to connect to server - {ex.Message}");
            return false;
        }
    }

    public async Task SendEventToServer<T>(Event<T> eventToSend)
    {
        var serializedEvent = JsonSerializer.Serialize(eventToSend);
        await WriteOnConnectionAsync(serializedEvent);
    }

    private async Task<string> ReadFromConnectionAsync()
    {
        var buffer = new byte[1_024];
        var messageBuilder = new StringBuilder();

        while (true)
        {
            var receivedBytes = await _connection.NetworkStream.ReadAsync(buffer);
            if (receivedBytes == 0)
            {
                break;
            }

            var jsonString = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
            Console.WriteLine($"[Client]: Data received: {jsonString}");
            messageBuilder.Append(jsonString);
        }

        return messageBuilder.ToString();
    }

    private async Task WriteOnConnectionAsync(string message)
    {
        try
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            await _connection.NetworkStream.WriteAsync(bytes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"something went wrong while sending: {ex}");
        }
    }
}