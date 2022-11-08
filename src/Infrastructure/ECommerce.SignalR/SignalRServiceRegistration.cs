using ECommerce.Application.Abstractions.Hubs;
using ECommerce.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.SignalR;

public static class SignalRServiceRegistration
{
    public static IServiceCollection AddSignalRServices(this IServiceCollection services)
    {
        services.AddTransient<IProductHubService, ProductHubService>();
        services.AddTransient<IOrderHubService, OrderHubService>();
        services.AddSignalR();
        return services;
    }
}