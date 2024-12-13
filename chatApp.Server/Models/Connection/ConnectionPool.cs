using System.Collections.Concurrent;

namespace ChatApp.Server.Models.Connection;

public class ConnectionPool
{
    private static ConnectionPool _instance;
    private ConnectionPool() {}

    public static ConnectionPool GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ConnectionPool();
        }
        return _instance;
    }
    
    public readonly ConcurrentDictionary<Guid, ClientConnection> connectedCliens = new ConcurrentDictionary<Guid, ClientConnection>();
}