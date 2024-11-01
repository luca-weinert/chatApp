using System.Net;
using System.Text;
using System.Text.Json;
using System.Windows;
using ChatApp.Client.Wpf.Connection;
using ChatApp.Client.Wpf.Message;
using ChatApp.Shared;
using ChatApp.Shared.User;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Client.Wpf
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            services.AddSingleton<IConnectionService, ConnectionService>();
            services.AddSingleton<IMessageService, MessageService>();

            // Establish server connection
            var connectionService = new ConnectionService();
            var ipEndpoint = new IPEndPoint(IPAddress.Parse("192.168.178.45"), 8080);
            var serverConnection = await connectionService.ConnectToServerAsync(ipEndpoint);

            // Register the server connection as a singleton in DI
            services.AddSingleton(serverConnection);
            ServiceProvider = services.BuildServiceProvider();

            // Send user info after connection is established
            await SendUserToServer(serverConnection);
        }

        private static async Task SendUserToServer(ServerConnection serverConnection)
        {
            var serializedUser = JsonSerializer.Serialize(new User { Name = "Luca Weinert" });
            var bytes = Encoding.ASCII.GetBytes(serializedUser);
            await serverConnection.NetworkStream.WriteAsync(bytes);
            Console.WriteLine($"Client: sent following user to server: {serializedUser}");
        }
    }
}