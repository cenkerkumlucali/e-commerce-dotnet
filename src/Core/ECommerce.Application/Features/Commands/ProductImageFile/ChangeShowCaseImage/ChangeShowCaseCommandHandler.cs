using ECommerce.Application.Repositories.ProductImageFile;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Commands.ProductImageFile.ChangeShowCaseImage;

public class ChangeShowCaseCommandHandler : IRequestHandler<ChangeShowCaseCommandRequest, ChangeShowCaseCommandResponse>
{
    private readonly IProductImageWriteRepository _productImageWriteRepository;

    public ChangeShowCaseCommandHandler(IProductImageWriteRepository productImageWriteRepository)
    {
        _productImageWriteRepository = productImageWriteRepository;
    }


    public async Task<ChangeShowCaseCommandResponse> Handle(ChangeShowCaseCommandRequest request,
        CancellationToken cancellationToken)
    {
        var query = _productImageWriteRepository.Table
            .Include(c => c.Products)
            .SelectMany(c => c.Products, (pif, p) => new
            {
                pif,
                p
            });
        var data = await query.FirstOrDefaultAsync(c => c.p.Id == Guid.Parse(request.ProductId) && c.pif.Showcase);
        if (data is not null)
            data.pif.Showcase = false;

        var image = await query.FirstOrDefaultAsync(c => c.pif.Id == Guid.Parse(request.ImageId));
        if (image is not null)
            image.pif.Showcase = true;
        await _productImageWriteRepository.SaveAsync();
        return new ChangeShowCaseCommandResponse();
    }
}