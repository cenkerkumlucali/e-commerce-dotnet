using ECommerce.Application.Abstractions.Hubs;
using ECommerce.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ECommerce.SignalR.HubServices;

public class OrderHubService : IOrderHubService
{
    private readonly IHubContext<OrderHub> _hubContext;

    public OrderHubService(IHubContext<OrderHub> hubContext)
    {
        _hubContext = hubContext;
    }
    public async Task OrderAddedMessageAsync(string message) => await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.OrderAddedMessage, message);
}