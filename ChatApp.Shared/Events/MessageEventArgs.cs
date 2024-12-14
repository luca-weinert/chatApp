using ChatApp.Shared.Model.Message;

namespace ChatApp.Shared.Events;

public class MessageEventArgs : EventArgs
{
    public Message Message { get; private set; }

    public MessageEventArgs(Message message)
    {
        Message = message;
    }
}