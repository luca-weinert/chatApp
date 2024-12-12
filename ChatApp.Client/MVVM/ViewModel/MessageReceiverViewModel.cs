using System.Collections.ObjectModel;
using System.ComponentModel;
using ChatApp.Client.Wpf.Services.MessageService;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.MVVM.ViewModel;

public class MessageReceiverViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Message> _receivedMessages; 
    private IMessageService _messageService;
    
    public ObservableCollection<Message> ReceivedMessages
    {
        get => _receivedMessages;
        set
        {
            _receivedMessages = value;
            OnPropertyChanged(nameof(ReceivedMessages));
        }
    }

    public MessageReceiverViewModel(IMessageService messageService)
    {
        _messageService = messageService;
      //  ReceivedMessages = new ObservableCollection<Message>
      //  {
      //      new Message(Guid.NewGuid(), Guid.NewGuid(), "123")
      //  };
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        _messageService.SendMessageAsync(new Message(Guid.NewGuid(), Guid.NewGuid(), "123")); 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
