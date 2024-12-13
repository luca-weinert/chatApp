using ChatApp.Server.Services.TcpEndpointService;
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
                services.AddSingleton<App>();
            });
    }
}

public class App()
{
    private readonly CancellationTokenSource _cts = new();

    public async Task InitializeApp()
    {
        var tcpEndpoint = TcpEndpoint.GetTcpEndpoint(); 
        await tcpEndpoint.StartAsync(_cts.Token);
    }
}