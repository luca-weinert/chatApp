using System.Windows.Controls;
using ChatApp.Shared.Message;

namespace ChatApp.Client.Wpf.MVVM.View;

public partial class MessageReceiverControl : UserControl
{
    public MessageReceiverControl()
    {
        InitializeComponent();
        List<Message> receivedMessages = new List<Message>();
        var message = new Message(Guid.NewGuid(), Guid.NewGuid(), "Test");
        var messageTwo = new Message(Guid.NewGuid(), Guid.NewGuid(), "Das ist ein Test");
        receivedMessages.Add(message);
        receivedMessages.Add(messageTwo);
        ReceivedMessages.ItemsSource = receivedMessages;
    }

    public void OnMessageReceived(object sender, EventArgs eventArgs)
    {
       
    }
}