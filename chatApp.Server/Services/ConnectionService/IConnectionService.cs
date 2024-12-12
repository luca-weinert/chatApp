using System.Net.Sockets;
using ChatApp.Shared.Model.Connection;

namespace chatApp_server.Services.ConnectionService;

public interface IConnectionService
{
    public IConnection GetConnectionForClient(TcpClient client);
    public IConnection UpdateConnection(IConnection connection);
    public Task<IConnection> GetConnectionByUserIdAsync(Guid userId);
    public Task<bool> isUserConnected(Guid userId);
}