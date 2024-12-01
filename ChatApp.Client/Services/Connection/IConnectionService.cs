using System.Net;

namespace ChatApp.Client.Wpf.Services.Connection;

public interface IConnectionService
{
    public Task<IServerConnection?> ConnectToServerAsync(IPEndPoint serverEndPoint);
    public void Disconnect();
}