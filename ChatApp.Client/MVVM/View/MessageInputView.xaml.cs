using System.Windows.Controls;
using ChatApp.Client.Wpf.MVVM.ViewModel;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.MVVM.View;

public partial class MessageInputView : UserControl
{
    public MessageInputView()
    {
        InitializeComponent();
     
    }

    public void SendMessage(Object source, EventArgs eventArgs)
    {
        var _viewModel = DataContext as MessageInputViewModel;
        var senderUserId = Guid.NewGuid();
        var targetUserId = Guid.NewGuid();
        var messageContent = MessageBoxInput.Text;
        var message = new Message(senderUserId, targetUserId, messageContent);
        _viewModel.SendMessage(message);
    }
}