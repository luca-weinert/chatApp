using ChatApp.Shared.Communication.Listener;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Shared.Communication;

public static class ServiceCollectionExtensions
{
    
    public static IServiceCollection AddCommunication(this IServiceCollection services)
    {
        services.AddSingleton<IListener, Listener.Listener>();
        return services;
    }
}
