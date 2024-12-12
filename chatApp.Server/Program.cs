﻿using chatApp_server.Endpoints;
using chatApp_server.Repositorys.Connection;
using chatApp_server.Repositorys.User;
using chatApp_server.Services.ConnectionService;
using chatApp_server.Services.ListenerService;
using chatApp_server.Services.MessageService;
using chatApp_server.Services.UserService;
using ChatApp.Shared;
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
                    var listener = new IListenerServiceService();
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