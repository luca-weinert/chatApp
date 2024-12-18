﻿using System.Net;
using System.Windows;
using ChatApp.Client.Wpf.Services.AuthenticationService;
using ChatApp.Client.Wpf.Services.ChatProtocolService;
using ChatApp.Client.Wpf.Services.ListenerService;
using ChatApp.Client.Wpf.Services.MessageService;

namespace ChatApp.Client.Wpf
{
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var serverIpEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            
            var cancellationTokenSource = new CancellationTokenSource();
            var authenticationService = AuthenticationService.Instance;
            var chatProtocolService = new ChatProtocolService();
            var listenerService = new ListenerService();
            var messageService = MessageService.Instance;

            listenerService.MessageReceived += messageService.OnMessageReceived;
            listenerService.MessageReceivedConformationReceived += messageService.OnMessageReceivedConfirmationReceived;
            
            try
            {
                await chatProtocolService.ConnectAsync(serverIpEndpoint);
                await authenticationService.AuthenticateUserAsync();
                var user = authenticationService.GetUser();
                Console.WriteLine($"[Client]: your userId is: {user.Id}");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Shutdown();
            }
            
            _ = Task.Run(() => listenerService.ListenOnServerConnection(cancellationTokenSource.Token));
        }
    }
}