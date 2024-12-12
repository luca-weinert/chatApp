namespace ChatApp.Client.Wpf.Model.Connection;

public interface IServerConnection
{
    public Task WriteAsync(string rawMessage);
    public Task<string> ReadAsync();
}