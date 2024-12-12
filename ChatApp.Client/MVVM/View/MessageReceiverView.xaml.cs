using System.Windows.Controls;
using ChatApp.Client.Wpf.MVVM.ViewModel;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.MVVM.View;

public partial class MessageReceiverView : UserControl
{
    public MessageReceiverView()
    {
        InitializeComponent();
    }
    
    public void OnMessageReceived(object sender, EventArgs eventArgs)
    {
        var vm = DataContext as MessageReceiverViewModel;
        if (vm != null)
        {
            vm.ReceivedMessages.Add(new Message(Guid.NewGuid(), Guid.NewGuid(), "New message"));
        } 
    }
}