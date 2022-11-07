using ECommerce.Application.Abstractions.Services;
using MediatR;

namespace ECommerce.Application.Features.Commands.Basket.UpdateQuantity;

public class UpdateQuantityCommandHandler:IRequestHandler<UpdateQuantityCommandRequest,UpdateQuantityCommandResponse>
{
    readonly IBasketService _basketService;

    public UpdateQuantityCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<UpdateQuantityCommandResponse> Handle(UpdateQuantityCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketService.UpdateQuantityAsync(new()
        {
            BasketItemId = request.BasketItemId,
            Quantity = request.Quantity
        });

        return new();
    }
}