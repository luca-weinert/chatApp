using System.Net;
using System.Windows;
using ChatApp.Client.Wpf.Services.AuthenticationService;
using ChatApp.Client.Wpf.Services.ChatProtocolService;

namespace ChatApp.Client.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Iockernel.Initialze(new IocConfiguration());
            base.OnStartup(e);
            
            var serverIpEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            var chatProtocolService = new ChatProtocolService();
            chatProtocolService.ConnectAsync(serverIpEndpoint);
            var authenticationService = AuthenticationService.Instance;
            authenticationService.AuthenticateUser();
        }
    }
}