using ECommerce.Application.Abstractions.Services;
using MediatR;

namespace ECommerce.Application.Features.Commands.Basket.AddItemToBasket;

public class AddItemToBasketCommandHandler:IRequestHandler<AddItemToBasketCommandRequest,AddItemToBasketCommandResponse>
{
    private readonly IBasketService _baskerService;

    public AddItemToBasketCommandHandler(IBasketService baskerService)
    {
        _baskerService = baskerService;
    }

    public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
    {
        await _baskerService.AddItemToBasketAsync(new()
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity
        });
        return new();
    }
}