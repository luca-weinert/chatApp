using chatApp_server.Connection;
using chatApp_server.Event;
using chatApp_server.Message;
using chatApp_server.User;
using ChatApp.Communication.Event;
using ChatApp.Shared.Connection;
using ChatApp.Shared.Listener;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace chatApp_server;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            await services.GetRequiredService<App>().InitializeApp(); // Add await
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
                services.AddSingleton<IConnection, ChatApp.Shared.Connection.Connection>();
                services.AddSingleton<IListener, Listener>();
                services.AddSingleton<IEventHandler, ServerEventHandler>();
                services.AddSingleton<IEventFactory, EventFactory>();
                services.AddSingleton<IUserRepository, UserRepository>();
                services.AddSingleton<IUserService, UserService>();
                services.AddSingleton<IMessageService, MessageService>();
                services.AddSingleton<Server.TcpEndpoint>();
                services.AddSingleton<App>();
            });
    }
}

public class App(Server.TcpEndpoint tcpEndpoint)
{
    private CancellationTokenSource _cts = new CancellationTokenSource();
    
    public async Task InitializeApp()
    {
        await tcpEndpoint.StartAsync(_cts.Token);
    }
}