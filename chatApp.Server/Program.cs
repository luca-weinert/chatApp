﻿using chatApp_server.Connection;
using chatApp_server.Endpoints;
using chatApp_server.Listener;
using chatApp_server.Message;
using chatApp_server.User;
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
                services.AddSingleton<IListener>((provider) =>
                {
                    var listener = new Listener.Listener();
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