using ECommerce.Application.Repositories;
using ECommerce.Application.Repositories.Product;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Features.Commands.Product.RemoveProduct;

public class RemoveProductCommandHandler: IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
{
    readonly IProductWriteRepository _productWriteRepository;
    readonly ILogger<RemoveProductCommandHandler> _logger;

    public RemoveProductCommandHandler(IProductWriteRepository productWriteRepository, ILogger<RemoveProductCommandHandler> logger)
    {
        _productWriteRepository = productWriteRepository;
        _logger = logger;
    }

    public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
    {
        await _productWriteRepository.RemoveAsync(request.Id);
        await _productWriteRepository.SaveAsync();
        _logger.LogInformation("Ürün silindi...");

        return new();
    }
}