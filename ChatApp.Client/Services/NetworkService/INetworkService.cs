using System.Net;

namespace ChatApp.Client.Wpf.Services.NetworkService;

public interface INetworkService
{
    public Task ConnectAsync(IPEndPoint ipEndPoint);
    public Task<string> ListenAsync();
    public Task SendAsnAsync(string message);
}