using ChatApp.Shared.Model.Connection;
using ChatApp.Shared.Model.User;

namespace ChatApp.Server.Events;

public class UserEventArgs : EventArgs
{
    public User User { get; private set; }
    public IConnection Connection { get; private set; }
    
    public UserEventArgs(IConnection connection, User user)
    {
        Connection = connection;
        User = user;
    }
}