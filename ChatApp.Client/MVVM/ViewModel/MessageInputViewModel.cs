using ChatApp.Client.Wpf.Services.MessageService;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.MVVM.ViewModel;

public class MessageInputViewModel
{
    private MessageService _messageService;

    public MessageInputViewModel(MessageService messageService)
    {
        _messageService = messageService;
    }

    public void SendMessage(Message message)
    {
        _messageService.SendMessageAsync(message);
    }
}