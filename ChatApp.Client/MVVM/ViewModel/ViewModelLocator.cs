namespace ChatApp.Client.Wpf.MVVM.ViewModel;

public class ViewModelLocator
{
    public MessageReceiverViewModel MessageReceiverViewModel
    {
        get => new MessageReceiverViewModel();
    }

    public MessageInputViewModel MessageInputViewModel
    {
        get => new MessageInputViewModel();
    }

    public StatusBarViewModel StatusBarViewModel
    {
        get => new StatusBarViewModel();
    }
}