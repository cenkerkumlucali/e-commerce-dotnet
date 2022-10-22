using ECommerce.Application.Repositories;
using ECommerce.Application.Repositories.ProductImageFile;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.ProductImageFile;

public class ProductImageWriteRepository:WriteRepository<Domain.Entities.ProductImageFile>,IProductImageWriteRepository
{
    public ProductImageWriteRepository(ECommerceDbContext context) : base(context)
    {
    }
}