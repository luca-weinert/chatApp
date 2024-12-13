using System.Net;
using ChatApp.ChatProtocol;

namespace ChatApp.Client.Wpf.Services.ChatProtocolService;

public class ChatProtocolService
{
    private NetworkService.NetworkService _networkService;
    
    public ChatProtocolService()
    {
        _networkService = new NetworkService.NetworkService();
    }

    public async Task ConnectAsync(IPEndPoint ip)
    {
        try
        {
            await _networkService.ConnectAsync(ip);
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

    public Task<ChatProtocolDataPackage> ListenAsync()
    {
        throw new NotImplementedException();
    }
}