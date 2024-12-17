using System.IO;
using ChatApp.Client.Wpf.Services.FileService;
using ChatApp.Client.Wpf.Services.MessageService;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.MVVM.ViewModel;

public class MessageInputViewModel
{
    private MessageService _messageService;
    private FileService _fileService;

    public MessageInputViewModel()
    {
        _messageService = MessageService.Instance;
        _fileService =  FileService.Instance;
    }

    public void SendMessage(Message message)
    {
        _ = _messageService.SendMessageAsync(message);
    }
    
    public async Task SendFileAsync(FileInfo fileInfo)
    {
        await _fileService.SendFileAsync(fileInfo, Guid.NewGuid(), Guid.NewGuid());
    }
}