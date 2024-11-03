using System.Windows;
using ChatApp.Client.Wpf.Message;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Client.Wpf
{
    public partial class MainWindow : Window
    {
        private string _messageInputValue;
        private readonly IMessageService _messageService;

        public MainWindow()
        {
            InitializeComponent(); 
            DataContext = new MainWindowViewModel();
            _messageService = App.ServiceProvider.GetRequiredService<IMessageService>();
        }

        private void ContactClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Das ist ein test");
            Console.WriteLine($"contact clicked by sender: {sender}, event: {e.RoutedEvent}");
        }

        private async void SendButtonClick(object sender, RoutedEventArgs e)
        {
            var message = new Shared.Message.Message(Guid.NewGuid(), Guid.NewGuid(), _messageInputValue);
            await _messageService.SendMessageAsync(message);
        }
    }
}