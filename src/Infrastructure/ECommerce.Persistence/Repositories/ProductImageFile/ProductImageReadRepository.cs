using ECommerce.Application.Repositories.ProductImageFile;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.ProductImageFile;

public class ProductImageReadRepository:ReadRepository<Domain.Entities.ProductImageFile>,IProductImageReadRepository
{
    public ProductImageReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}