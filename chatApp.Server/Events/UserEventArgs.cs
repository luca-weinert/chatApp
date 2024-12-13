using ChatApp.Server.Models.Connection;
using ChatApp.Shared.Model.User;

namespace ChatApp.Server.Events;

public class UserEventArgs : EventArgs
{
    public User User { get; private set; }
    
    public UserEventArgs(User user)
    {
        User = user;
    }
}