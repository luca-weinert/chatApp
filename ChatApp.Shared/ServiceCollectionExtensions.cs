using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        return services;
    }
}