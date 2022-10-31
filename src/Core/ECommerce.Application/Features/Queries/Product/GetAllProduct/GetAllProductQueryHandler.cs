using ECommerce.Application.Repositories;
using MediatR;

namespace ECommerce.Application.Features.Queries.Product.GetAllProduct;

public class GetAllProductQueryHandler:IRequestHandler<GetAllProductQueryRequest,GetAllProductQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        var totalCount = _productReadRepository.GetAll(false).Count();
        var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size)
            .Take(request.Size).Select(c => new
            {
                c.Id,
                c.Name,
                c.Stock,
                c.Price,
                c.CreatedDate,
                c.UpdatedDate
            });
        return new()
        {
            Products = products,
            TotalCount = totalCount
        };
    }
}