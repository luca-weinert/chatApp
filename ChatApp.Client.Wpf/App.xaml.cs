using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using chatApp_server.Message;
using ChatApp.Client.Wpf.Connection;
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
        var connectionService = new ConnectionService();
        var ipEndPoint = new IPEndPoint(IPAddress.Parse("172.26.224.1"), 8080);
        var serverConnection = await connectionService.ConnectToAsync(ipEndPoint);
        
        var serializedUser = JsonSerializer.Serialize(new User{ Name = "Luca Weinert"});
        var bytes = Encoding.ASCII.GetBytes(serializedUser);
        await serverConnection.NetworkStrean.WriteAsync(bytes);
        
        Console.WriteLine($"Client: sent following user to server: {serializedUser}");
    }
}