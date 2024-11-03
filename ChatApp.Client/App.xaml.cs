using System.Windows;
using ChatApp.Client.Wpf.Communication;
using ChatApp.Client.Wpf.Connection;
using ChatApp.Client.Wpf.Event;
using ChatApp.Client.Wpf.Message;
using ChatApp.Client.Wpf.User;
using ChatApp.Communication;
using Microsoft.Extensions.DependencyInjection;
using ICommunicationService = ChatApp.Client.Wpf.Communication.ICommunicationService;

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
            var communicationService = ServiceProvider.GetService<ICommunicationService>();
            Task.Run(() => communicationService.HandleCommunicationAsync());
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConnectionService, ConnectionService>();
            services.AddSingleton<IEventHandler, CustomEventHandler>();
            services.AddSingleton<IEventFactory, EventFactory>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<ICommunicationService, CommunicationService>();
        }
    }
}