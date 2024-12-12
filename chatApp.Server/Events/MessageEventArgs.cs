using ChatApp.Shared.Model.Message;

namespace ChatApp.Server.Events;

public class MessageEventArgs : EventArgs
{
    public Message Message { get; private set; }

    public MessageEventArgs(Message message)
    {
        Message = message;
    }
}