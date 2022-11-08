using ECommerce.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;

namespace ECommerce.SignalR;

public static class HubRegistration
{
    public static void MapHubs(this WebApplication app)
    {
        app.MapHub<ProductHub>("/products-hub");
        app.MapHub<OrderHub>("/orders-hub");
    }
}