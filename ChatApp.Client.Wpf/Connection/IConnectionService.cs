using System.Net;

namespace ChatApp.Client.Wpf.Connection;

public interface IConnectionService
{ 
    public Task<Shared.Connection?> ConnectToServerAsync(IPEndPoint serverEndPoint);
}