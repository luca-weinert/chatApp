using System.Collections.Concurrent;

namespace ChatApp.Server.Models.Connection;

public class ClientConnectionPool
{
    private static ClientConnectionPool instance = null;
    private static readonly object padlock = new object();

    private ClientConnectionPool()
    {
    }

    public static ClientConnectionPool Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ClientConnectionPool();
                }

                return instance;
            }
        }
    }

    public readonly ConcurrentDictionary<Guid, ClientConnection> connectedCliens =
        new ConcurrentDictionary<Guid, ClientConnection>();
}