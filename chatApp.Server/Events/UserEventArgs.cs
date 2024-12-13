using ChatApp.Shared.Model.User;

namespace ChatApp.Server.Events;

public class UserEventArgs : EventArgs
{
    public User User { get; private set; }
    public Guid ConnectionID { get; private set; }
    
    public UserEventArgs(User user, Guid connectionID)
    {
        User = user;
        ConnectionID = connectionID;
    }
}