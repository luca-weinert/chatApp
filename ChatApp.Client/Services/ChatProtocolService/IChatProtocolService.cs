using System.Net;
using ChatApp.ChatProtocol;

namespace ChatApp.Client.Wpf.Services.ChatProtocolService;

public interface IChatProtocolService
{
    public Task ConnectAsync(IPEndPoint endpoint);
    public Task SendAsync(ChatProtocolDataPackage chatProtocolDataPackage);
    public Task<ChatProtocolDataPackage> ListenAsync();
}