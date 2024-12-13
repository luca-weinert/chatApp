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

    public void SendMessage(Object source, EventArgs eventArgs)
    {
        var _viewModel = DataContext as MessageInputViewModel;
        var authService = AuthenticationService.Instance;
        var user = authService.GetUser();
        var senderUserId = user.Id;
        var targetUserId = user.Id;
        var messageContent = MessageBoxInput.Text;
        var message = new Message(senderUserId, targetUserId, messageContent);
        _viewModel.SendMessage(message);
    }
}