using Newtonsoft.Json;

namespace ChatApp.Shared.Model.Message;

public class MessageReceivedConfirmation : EventArgs
{
    public MessageReceivedConfirmation()
    {
        
    }
    public MessageReceivedConfirmation(Message receivedMessage)
    {
        ReceivedMessage = receivedMessage;
    }

    public Message ReceivedMessage { get; init; }
    
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}