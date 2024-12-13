using System.Collections.Concurrent;

namespace ChatApp.Server.Models.User;

public class UserPool
{
    
    private UserPool() {}
    private static readonly object padlock = new object();
    private static UserPool _instance;
    
    public readonly ConcurrentDictionary<Guid, Shared.Model.User.User> users =
        new ConcurrentDictionary<Guid, Shared.Model.User.User>();
    
    public static UserPool Instance
    {
        get
        {
            lock (padlock)
            {
                if (_instance == null)
                {
                    _instance = new UserPool();
                }
                return _instance;
            }
        }
    }
}