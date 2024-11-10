namespace chatApp_server.Events;

public class MessageEventArgs : EventArgs
{
    private readonly ChatApp.Shared.Message.Message _message;
    
    public MessageEventArgs(ChatApp.Shared.Message.Message message)
    {
        _message = message;
    }

    public ChatApp.Shared.Message.Message Message
    {
        get { return _message; }
    }
}