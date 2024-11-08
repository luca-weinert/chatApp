using ChatApp.Communication.Listener;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Communication;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventing(this IServiceCollection services)
    {
        services.AddSingleton<IListener, Listener.Listener>();
        return services;
    }
}