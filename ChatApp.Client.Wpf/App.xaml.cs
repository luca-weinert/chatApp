using System.Text;
using System.Text.Json;
using System.Windows;
using ChatApp.Shared;

namespace ChatApp.Client.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var tcpConnector = new TcpConnector();
        var connection = await tcpConnector.GetConnectionToServer();
        Console.WriteLine("Connection to server established");
        var serializedUser = JsonSerializer.Serialize(new User{ Name = "Luca Weinert"});
        var bytes = Encoding.ASCII.GetBytes(serializedUser);
        await connection.NetworkStrean.WriteAsync(bytes);
        Console.WriteLine($"Client: sent following user to server: {serializedUser}");
    }
}