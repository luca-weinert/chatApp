namespace ChatApp.Shared.Connection;

public interface IConnection
{
    public Task<string> ReadAsync();
    public Task<bool> WriteAsync(string data);
    public void Dispose();
}