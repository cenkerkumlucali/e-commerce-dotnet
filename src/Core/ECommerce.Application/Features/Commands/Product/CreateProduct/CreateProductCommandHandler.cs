using ECommerce.Application.Abstractions.Hubs;
using ECommerce.Application.Repositories;
using MediatR;

namespace ECommerce.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandHandler:IRequestHandler<CreateProductCommandRequest,CreateProductCommandResponse>
{
    private IProductWriteRepository _productWriteRepository;
    private IProductHubService _productHubService;

    public CreateProductCommandHandler(
        IProductWriteRepository productWriteRepository,
        IProductHubService productHubService
        )
    {
        _productWriteRepository = productWriteRepository;
        _productHubService = productHubService;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productWriteRepository.AddAsync(new ()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        });
        await _productWriteRepository.SaveAsync();
        await _productHubService.ProductAddedMessageAsync($"{request.Name} isminde ürün eklenmiştir.");
        return new();
    }
}