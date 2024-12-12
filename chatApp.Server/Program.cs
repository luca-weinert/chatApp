using ChatApp.Server.Endpoints;
using ChatApp.Server.Repositorys.Connection;
using ChatApp.Server.Repositorys.User;
using ChatApp.Server.Services.ConnectionService;
using ChatApp.Server.Services.ListenerService;
using ChatApp.Server.Services.MessageService;
using ChatApp.Server.Services.UserService;
using ChatApp.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatApp.Server;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            await services.GetRequiredService<App>().InitializeApp();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    private static IHostBuilder CreateHostBuilder(string[] strings)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<IConnectionRepository, ConnectionRepository>();
                services.AddSingleton<IConnectionService, ConnectionService>();
                services.AddShared();
                services.AddSingleton<IMessageService, MessageService>();
                services.AddSingleton<IUserRepository, UserRepository>();
                services.AddSingleton<IListenerService>((provider) =>
                {
                    var listener = new ListenerServiceService();
                    var userService = provider.GetService<IUserService>()!;
                    var messageService = provider.GetService<IMessageService>()!;
                    
                    listener.UserReceived += userService.OnUserInformationReceived; 
                    listener.MessageReceived += messageService.OnMessageReceived;
                    
                    return listener;
                });
                services.AddSingleton<IUserService, UserService>();
                services.AddSingleton<IEndpoint, TcpEndpoint>();
                services.AddSingleton<App>();
            });
    }
}

public class App(IEndpoint endpoint)
{
    private readonly CancellationTokenSource _cts = new();

    public async Task InitializeApp()
    {
        await endpoint.StartAsync(_cts.Token);
    }
}