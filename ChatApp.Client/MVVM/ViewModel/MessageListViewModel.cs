using System.Collections.ObjectModel;
using System.ComponentModel;
using ChatApp.Client.Wpf.Services.MessageService;
using ChatApp.Shared.Events;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.MVVM.ViewModel;

public class MessageReceiverViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Message> _receivedMessages = [];
    private readonly MessageService _messageService;

    public MessageReceiverViewModel()
    {
        _messageService = MessageService.Instance;
        _messageService.MessageReceived += OnMessageReceived;
    }

    public ObservableCollection<Message> ReceivedMessages
    {
        get => _receivedMessages;
        set
        {
            _receivedMessages = value;
            OnPropertyChanged(nameof(ReceivedMessages));
        }
    }

    private void OnMessageReceived(object? sender, MessageEventArgs messageEventArgs)
    {
        if (messageEventArgs?.Message == null) return;

        if (System.Windows.Application.Current.Dispatcher.CheckAccess())
        {
            _receivedMessages.Add(messageEventArgs.Message);
        }
        else
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                _receivedMessages.Add(messageEventArgs.Message);
            });
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}