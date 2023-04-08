using Microsoft.Extensions.DependencyInjection;

namespace Du.Netty;

public static class NettyExtensions
{
    public static IServiceCollection AddNetty(this IServiceCollection services)
    {
        services.AddHostedService<NettyService>();
        return services;
    }
}