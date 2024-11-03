namespace ChatApp.Shared.Connection;

public interface IConnection
{
    public Task<string> ReadAsync(CancellationToken cancellationToken);
    public Task<bool> WriteAsync(string data);
    public void Dispose();
}