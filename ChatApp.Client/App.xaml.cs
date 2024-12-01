using System.Windows;
using ChatApp.Client.Wpf.Services.Communication;
using ChatApp.Client.Wpf.Services.Connection;
using ChatApp.Client.Wpf.Services.Listener;
using ChatApp.Client.Wpf.Services.Message;
using Microsoft.Extensions.DependencyInjection;
using ICommunicationService = ChatApp.Client.Wpf.Services.Communication.ICommunicationService;

namespace ChatApp.Client.Wpf
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
            var communicationService = ServiceProvider.GetService<ICommunicationService>()!;
            Task.Run(() => communicationService.HandleCommunicationAsync());
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConnectionService, ConnectionService>();
            services.AddSingleton<IListener, Listener>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<ICommunicationService, CommunicationService>();
        }
    }
}