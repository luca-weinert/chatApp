using System.Windows;

namespace ChatApp.Client.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string _messageInputValue;
    
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
        Console.WriteLine($"message input: {_messageInputValue}");
    }
}