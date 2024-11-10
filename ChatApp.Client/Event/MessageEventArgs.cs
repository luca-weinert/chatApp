namespace ChatApp.Client.Wpf.Event;

public class MessageEventArgs : EventArgs
{
    private readonly Shared.Message.Message _message;
    
    public MessageEventArgs(Shared.Message.Message message)
    {
        _message = message;
    }

    public Shared.Message.Message Message
    {
        get { return _message; }
    }
}