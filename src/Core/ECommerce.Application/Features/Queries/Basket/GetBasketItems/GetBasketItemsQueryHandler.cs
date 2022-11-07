using ECommerce.Application.Abstractions.Services;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Queries.Basket.GetBasketItems;

public class GetBasketItemsQueryHandler:IRequestHandler<GetBasketItemsQueryRequest,List<GetBasketItemsQueryResponse>>
{
    readonly IBasketService _basketService;

    public GetBasketItemsQueryHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
    {
        List<BasketItem> basketItems = await _basketService.GetBasketItemsAsync();
        return basketItems.Select(ba => new GetBasketItemsQueryResponse
        {
            BasketItemId = ba.Id.ToString(),
            Name = ba.Product.Name,
            Price = ba.Product.Price,
            Quantity = ba.Quantity
        }).ToList();
    }
}