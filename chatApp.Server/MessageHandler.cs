using System.Text;
using System.Text.Json;
using ChatApp.Shared;

namespace chatApp_server;

public class MessageHandler
{
    private ConnectionHandler _connectionHandler;
    public MessageHandler(ConnectionHandler connectionHandler)
    {
        _connectionHandler = connectionHandler;
    }

    public async Task HandleMessage(string rawMessage)
    {
        var message = JsonSerializer.Deserialize<Message>(rawMessage);
        Console.WriteLine($"Received message: {rawMessage}");
    }
    
    public async Task SendAsync(Message message, Connection targetConnection)
    {
        if (_connectionHandler.IsClientConnected(targetConnection))
        {
            var rawMessage = JsonSerializer.Serialize(message);
            var messageBytes = Encoding.UTF8.GetBytes(rawMessage);
            await targetConnection.Stream.WriteAsync(messageBytes);
        }
        else
        {
            Console.WriteLine("target client not connected");
        }
    }
}