using System.Windows;
using ChatApp.Client.Wpf.Message;

namespace ChatApp.Client.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string _messageInputValue;
    private readonly IMessageService _messageService;
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void ContactClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Das ist ein test");
        Console.WriteLine($"contact clicked by sender: {sender}, event: {e.RoutedEvent}");
    }

    private void SendButtonClick(object sender, RoutedEventArgs e)
    {
        _messageInputValue = MessageTextBox.Text;

        var message = new Shared.Message(Guid.NewGuid(), Guid.NewGuid(), _messageInputValue);
        _messageService.Send(message);
        
        Console.WriteLine($"message input: {_messageInputValue}");
    }
}