using ChatApp.ChatProtocol.Helper;
using ChatApp.ChatProtocol.Models;
using ChatApp.Server.Models.Connection;

namespace ChatApp.Server.Services.ChatProtocolService;

public class ChatProtocolService
{
    private readonly NetworkService.NetworkService _networkService;
    private readonly ClientConnection _clientConnection;
    
    public ChatProtocolService(ClientConnection clientConnection)
    {
        _clientConnection = clientConnection;
        _networkService = new NetworkService.NetworkService(clientConnection);
    }
    
    public async Task SendAsync(ChatProtocolDataPackage chatProtocolDataPackage)
    {
        if (_clientConnection == null) throw new Exception("clientConnection is null");
        await _networkService.WriteAsync(chatProtocolDataPackage.ToJson());
    }

    public async Task<ChatProtocolDataPackage?> ListenAsync()
    {
        if (_clientConnection == null) throw new Exception("clientConnection is null");
        var receivedData = await _networkService.ListenAsync();
        var chatProtocolDataPackage = ChatProtocolHelper.Deserialize(receivedData);
        return chatProtocolDataPackage;
    }
}