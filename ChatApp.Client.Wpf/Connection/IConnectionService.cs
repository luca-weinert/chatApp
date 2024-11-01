using System.Net;

namespace ChatApp.Client.Wpf.Connection;

public interface IConnectionService
{ 
    public Task<ServerConnection?> ConnectToServerAsync(IPEndPoint serverEndPoint);
}