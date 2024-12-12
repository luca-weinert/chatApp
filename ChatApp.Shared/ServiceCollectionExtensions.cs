using ChatApp.Shared.Model.Connection;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddSingleton<IConnection, Connection>();
        return services;
    }
}