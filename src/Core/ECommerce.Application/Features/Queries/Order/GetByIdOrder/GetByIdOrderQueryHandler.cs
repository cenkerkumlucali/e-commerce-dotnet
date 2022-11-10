using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.DTOs.Order;
using MediatR;

namespace ECommerce.Application.Features.Queries.Order.GetByIdOrder;

public class GetByIdOrderQueryHandler:IRequestHandler<GetByIdOrderQueryRequest,GetByIdOrderQueryResponse>
{
    private readonly IOrderService _orderService;

    public GetByIdOrderQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<GetByIdOrderQueryResponse> Handle(GetByIdOrderQueryRequest request, CancellationToken cancellationToken)
    {
        SingleOrder data = await _orderService.GetOrderByIdAsync(request.Id);
        return new GetByIdOrderQueryResponse
        {
            Id = data?.Id,
            BasketItems = data?.BasketItems,
            Address = data.Address,
            CreatedDate = data.CreatedDate,
            Description = data.Description,
            OrderCode = data.OrderCode,
            Completed = data.Completed
        };
    }
}