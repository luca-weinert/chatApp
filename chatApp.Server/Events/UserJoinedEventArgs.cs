namespace chatApp_server.Events;

public class UserJoinedEventArgs : EventArgs
{
    private Guid UserId { get; set; }
    
    public UserJoinedEventArgs(Guid userid)
    {
        UserId = userid;
    }
    
    public Guid Userid => UserId;
}