using System.Data;
using ChatApp.Shared.Connection;

namespace chatApp_server.Events;

public class UserEventArgs : EventArgs
{
    public ChatApp.Shared.User.User User { get; private set; }
    public IConnection Connection { get; private set; }
    
    public UserEventArgs(IConnection connection, ChatApp.Shared.User.User user)
    {
        Connection = connection;
        User = user;
    }
}