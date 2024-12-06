namespace ChatApp.Client.Wpf.MVVM.ViewModel;

public class ViewModelLocator
{
    public MessageReceiverViewModel MessageReceiverViewModel
    {
        get { return Iockernel.Get<MessageReceiverViewModel>(); }
    }
}