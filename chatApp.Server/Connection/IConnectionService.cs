using System.Data;
using System.Net.Sockets;

namespace chatApp_server.Connection;

public interface IConnectionService
{
    public Task<ClientConnection> GetConnectionForClientAsync(TcpClient client);
}