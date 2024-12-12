using ChatApp.Server.Models.Connection;
using ChatApp.Shared.Model.User;

namespace ChatApp.Server.Events;

public class UserEventArgs : EventArgs
{
    public User User { get; private set; }
    public IClientConnection Connection { get; private set; }
    
    public UserEventArgs(IClientConnection connection, User user)
    {
        Connection = connection;
        User = user;
    }
}