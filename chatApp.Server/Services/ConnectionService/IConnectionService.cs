using System.Net.Sockets;
using ChatApp.Server.Models.Connection;
using ChatApp.Shared.Model.Connection;

namespace ChatApp.Server.Services.ConnectionService;

public interface IConnectionService
{
    public IClientConnection GetConnectionForClient(TcpClient client);
    public IClientConnection UpdateConnection(IClientConnection connection);
    public Task<IClientConnection> GetConnectionByUserIdAsync(Guid userId);
    public Task<bool> isUserConnected(Guid userId);
}