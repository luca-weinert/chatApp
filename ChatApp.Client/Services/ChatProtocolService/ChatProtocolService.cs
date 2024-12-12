using System.Net;
using ChatApp.ChatProtocol;
using ChatApp.Client.Wpf.Services.NetworkService;

namespace ChatApp.Client.Wpf.Services.ChatProtocolService;

public class ChatProtocolService : IChatProtocolService
{
    private INetworkService _networkService;

    public ChatProtocolService(INetworkService networkService)
    {
        _networkService = networkService;
    }

    public async Task ConnectAsync(IPEndPoint endPoint)
    {
        try
        {
            await _networkService.ConnectAsync(endPoint);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SendAsync(ChatProtocolDataPackage chatProtocolDataPackage)
    {
        var serializedProtocolData = ChatProtocolHelper.Serialize(chatProtocolDataPackage);
        await _networkService.SendAsnAsync(serializedProtocolData);
    }

    public Task<ChatProtocolDataPackage> ListenAsync()
    {
        throw new NotImplementedException();
    }
}