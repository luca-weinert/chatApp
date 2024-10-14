using chatApp_server.Connection;
using chatApp_server.user;
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
                services.AddSingleton<IUserRepository, UserRepository>();
                services.AddSingleton<UserService>();
                services.AddSingleton<Server.TcpEndpoint>();
                services.AddSingleton<IConnectionRepository, ConnectionRepository>();
                services.AddSingleton<ConnectionService>();
                services.AddSingleton<App>();
            });
    }
}

public class App(Server.TcpEndpoint tcpEndpoint)
{
    public async Task InitializeApp()
    {
        await tcpEndpoint.StartAsync();
    }
}