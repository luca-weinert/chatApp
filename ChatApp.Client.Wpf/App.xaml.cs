using System.Net;
using System.Windows;
using ChatApp.Client.Wpf.Communication;
using ChatApp.Client.Wpf.Connection;
using ChatApp.Client.Wpf.Message;
using ChatApp.Client.Wpf.User;
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
            services.AddSingleton<ICommunicationService, CommunicationService>();
            services.AddSingleton<IMessageService, MessageService>();

            // Establish server connection
            var connectionService = new ConnectionService();
            var ipEndpoint = new IPEndPoint(IPAddress.Parse("192.168.178.45"), 8080);
            var serverConnection = await connectionService.ConnectToServerAsync(ipEndpoint);

            // Register the server connection as a singleton in DI
            services.AddSingleton(serverConnection);
            ServiceProvider = services.BuildServiceProvider();

            // Send user info after connection is established
            if (serverConnection != null) await SendUserToServer();
        }

        private static async Task SendUserToServer()
        { 
            var user = new Shared.User.User("Luca");
            var authenticationService = ServiceProvider.GetRequiredService<IAuthenticationService>();
            await authenticationService.AuthenticateUserAsync(user);
        }
    }
}