using System.Data;
using System.Net.Sockets;

namespace chatApp_server.Connection;

public interface IConnectionService
{
    public Task<ChatApp.Shared.Connection.Connection> GetConnectionForClientAsync(TcpClient client);
}