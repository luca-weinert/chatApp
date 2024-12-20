using System.Collections.Concurrent;

namespace ChatApp.Server.Models.User;

public class ActiveConnectedUserPool
{
    
    private ActiveConnectedUserPool() {}
    private static readonly object Padlock = new object();
    private static ActiveConnectedUserPool? _instance = null;
    
    public readonly ConcurrentDictionary<Guid, Shared.Model.User.User> Users =
        new ConcurrentDictionary<Guid, Shared.Model.User.User>();
    
    public static ActiveConnectedUserPool Instance
    {
        get
        {
            lock (Padlock)
            {
                return _instance ??= new ActiveConnectedUserPool();
            }
        }
    }
}