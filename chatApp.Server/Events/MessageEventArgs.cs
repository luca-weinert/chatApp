using ChatApp.Shared.Model.Message;

namespace chatApp_server.Events;

public class MessageEventArgs : EventArgs
{
    public Message Message { get; private set; }

    public MessageEventArgs(Message message)
    {
        Message = message;
    }
}