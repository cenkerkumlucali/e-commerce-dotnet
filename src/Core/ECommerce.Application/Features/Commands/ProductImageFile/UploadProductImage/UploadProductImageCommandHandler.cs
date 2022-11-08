using ECommerce.Application.Abstractions.Storage;
using ECommerce.Application.Repositories;
using ECommerce.Application.Repositories.Product;
using ECommerce.Application.Repositories.ProductImageFile;
using MediatR;

namespace ECommerce.Application.Features.Commands.ProductImageFile.UploadProductImage;

public class
    UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest,
        UploadProductImageCommandResponse>
{
    readonly IStorageService _storageService;
    readonly IProductReadRepository _productReadRepository;
    readonly IProductImageWriteRepository _productImageFileWriteRepository;

    public UploadProductImageCommandHandler(IStorageService storageService,
        IProductReadRepository productReadRepository, IProductImageWriteRepository productImageFileWriteRepository)
    {
        _storageService = storageService;
        _productReadRepository = productReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
    }

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request,
        CancellationToken cancellationToken)
    {
        List<(string fileName, string pathOrContainerName)> result =
            await _storageService.UploadAsync("photo-images", request.Files);


        Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);


        await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.ProductImageFile
        {
            FileName = r.fileName,
            Path = r.pathOrContainerName,
            Storage = _storageService.StorageName,
            Products = new List<Domain.Entities.Product>() { product }
        }).ToList());

        await _productImageFileWriteRepository.SaveAsync();

        return new();
    }
}