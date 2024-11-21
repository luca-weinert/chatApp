namespace chatApp_server.Events;

public class MessageEventArgs : EventArgs
{
    public ChatApp.Shared.Message.Message Message { get; private set; }

    public MessageEventArgs(ChatApp.Shared.Message.Message message)
    {
        Message = message;
    }
}