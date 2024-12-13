using System.Net;
using ChatApp.ChatProtocol.Helper;
using ChatApp.ChatProtocol.Models;

namespace ChatApp.Client.Wpf.Services.ChatProtocolService;

public class ChatProtocolService
{
    private NetworkService.NetworkService _networkService;
    
    public ChatProtocolService()
    {
        _networkService = NetworkService.NetworkService.Instance;
    }

    public async Task ConnectAsync(IPEndPoint ipEndPoint)
    {
        try
        {
            await _networkService.ConnectAsync(ipEndPoint);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SendAsync(ChatProtocolDataPackage chatProtocolDataPackage)
    {
        await _networkService.SendAsnAsync(chatProtocolDataPackage.ToJson());
    }

    public async Task<ChatProtocolDataPackage> ListenAsync()
    {
        var receivedData = await _networkService.ListenAsync();
        var chatProtocolDataPackage = ChatProtocolHelper.Deserialize(receivedData);
        return chatProtocolDataPackage;
    }
}