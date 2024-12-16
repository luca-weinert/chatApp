using System.Windows.Controls;
using ChatApp.Client.Wpf.MVVM.ViewModel;
using ChatApp.Client.Wpf.Services.AuthenticationService;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.MVVM.View;

public partial class MessageInputView : UserControl
{
    public MessageInputView()
    {
        InitializeComponent();
    }

    public void SendMessage(object source, EventArgs eventArgs)
    {
        // if (!string.IsNullOrEmpty(TargetUserIdInput.Text) && !string.IsNullOrEmpty(MessageBoxInput.Text)) return;
        var viewModel  = (MessageInputViewModel)DataContext;
        
        var authService = AuthenticationService.Instance;
        var user = authService.GetUser();
        
        var senderUserId = user.Id;
        var targetUserId = new Guid(TargetUserIdInput.Text);
        var messageContent = MessageBoxInput.Text;
        var message = new Message(senderUserId, targetUserId, messageContent);
        viewModel.SendMessage(message);
    }
}