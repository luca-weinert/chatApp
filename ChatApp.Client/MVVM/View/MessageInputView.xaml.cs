using System.Windows.Controls;
using ChatApp.Client.Wpf.Services.Message;
using ChatApp.Shared.Message;
using Ninject;

namespace ChatApp.Client.Wpf.MVVM.View;

public partial class MessageInputView : UserControl
{
    public MessageInputView()
    {
        InitializeComponent();
    }

    public void SendMessage(Object source, EventArgs eventArgs)
    {
        var testMsg = new Message(Guid.NewGuid(), Guid.NewGuid(), "test");
    }
}