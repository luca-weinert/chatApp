using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using ChatApp.Client.Wpf.MVVM.ViewModel;
using ChatApp.Client.Wpf.Services.AuthenticationService;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.MVVM.View;

public partial class MessageInputView : UserControl
{
    public MessageInputView()
    {
        InitializeComponent();
    }

    public void SendMessage(object source, EventArgs eventArgs)
    {
        if (string.IsNullOrWhiteSpace(TargetUserIdInput.Text) || string.IsNullOrWhiteSpace(MessageBoxInput.Text))
        {
            MessageBox.Show("Target User ID or Message content is empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!Guid.TryParse(TargetUserIdInput.Text, out var targetUserId))
        {
            MessageBox.Show("Invalid Target User ID format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var viewModel = DataContext as MessageInputViewModel;
        if (viewModel == null)
        {
            MessageBox.Show("Failed to access ViewModel.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var authService = AuthenticationService.Instance;
        var user = authService.GetUser();
        
        var senderUserId = user.Id;
        var messageContent = MessageBoxInput.Text;

        var message = new Message(senderUserId, targetUserId, messageContent);
        viewModel.SendMessage(message);
    }

    private async void SelectFile(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            var fileName = openFileDialog.FileName;
            var fileInfo = new FileInfo(fileName);
            FileInput.Text = fileInfo.FullName;

            var viewModel = DataContext as MessageInputViewModel;
            if (viewModel == null)
            {
                MessageBox.Show("Failed to access ViewModel.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                await viewModel.SendFileAsync(fileInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"File sending failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
