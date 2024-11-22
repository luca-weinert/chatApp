using ChatApp.SuperProtocol;

namespace ChatApp.Client.Wpf.Connection;

public interface IServerConnection
{
    public Task WriteAsync(SuperProtocolDataPackage package);
    public Task<string> ReadAsync();
}