using MediatR;

namespace ECommerce.Application.Features.Queries.Basket.GetBasketItems;

public class GetBasketItemsQueryRequest: IRequest<List<GetBasketItemsQueryResponse>>
{
}