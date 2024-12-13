using System.Text.Json;
using ChatApp.ChatProtocol;
using ChatApp.Server.Models.Connection;

namespace ChatApp.Server.Services.ChatProtocolService;

public class ChatProtocolService
{
    private NetworkService.NetworkService networkService;
    private ClientConnection clientConnection;
    
    public ChatProtocolService(ClientConnection clientConnection)
    {
        this.clientConnection = clientConnection;
        networkService = new NetworkService.NetworkService(clientConnection);
    }
    
    public async Task SendAsync(ChatProtocolDataPackage chatProtocolDataPackage)
    {
        if (clientConnection == null) throw new Exception("clientConnection is null");
        await networkService.WriteAsync(chatProtocolDataPackage.ToJson());
        Console.WriteLine("Sent Chat Protocol Data");
    }

    public async Task<ChatProtocolDataPackage> ListenAsync()
    {
        if (clientConnection == null) throw new Exception("clientConnection is null");
        var receivedData = await networkService.ListenAsync();
        var chatProtocolDataPackage = JsonSerializer.Deserialize<ChatProtocolDataPackage>(receivedData);
        return chatProtocolDataPackage;
    }
}