using System.Net.Sockets;

namespace ChatApp.Shared.Model.Connection;

public interface IConnection
{
    public Guid Id { get; }
    public Guid UserId { get; set; }
    public TcpClient Client { get; }
    public Stream NetworkStream { get; }
    public Task<string> ReadAsync(CancellationToken cancellationToken);
    public Task<bool> WriteAsync(string data);
}