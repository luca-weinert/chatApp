using ChatApp.SuperProtocol;

namespace ChatApp.Client.Wpf.Services.Connection;

public interface IServerConnection
{
    public Task WriteAsync(SuperProtocolDataPackage package);
    public Task<string> ReadAsync();
}