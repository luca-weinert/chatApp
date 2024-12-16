using ChatApp.Shared.Model.Message;

namespace ChatApp.Shared.Events;

public class MessageReceivedConformationEventArgs
{
    public MessageReceivedConfirmation ReceivedMessage { get; private set; }

    public MessageReceivedConformationEventArgs(MessageReceivedConfirmation receivedMessage)
    {
        ReceivedMessage = receivedMessage;
    }
}