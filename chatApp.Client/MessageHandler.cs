using System.Text.Json;
using ChatApp.Shared;

namespace chatApp_client;

public class MessageHandler
{
    private readonly TcpConnection _tcpConnection;

    public MessageHandler(TcpConnection tcpConnection)
    {
        _tcpConnection = tcpConnection;
    }

    // Asynchronously sends the Message object as a JSON string
    public async Task SendMessageAsync(Message message)
    {
        // Serialize the message object to JSON
        var messageJson = JsonSerializer.Serialize(message);

        // Send the JSON string over the TcpConnection
        await _tcpConnection.SendMessageAsync(messageJson);
    }
}