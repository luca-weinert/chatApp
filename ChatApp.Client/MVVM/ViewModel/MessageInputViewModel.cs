using ChatApp.Client.Wpf.Services.Message;
using ChatApp.Shared.Message;

namespace ChatApp.Client.Wpf.MVVM.ViewModel;

public class MessageInputViewModel
{
    private IMessageService _messageService;

    public MessageInputViewModel(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public void SendMessage(Message message)
    {
        _messageService.SendMessageAsync(message);
    }
}