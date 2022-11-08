using ECommerce.Application.Repositories;
using ECommerce.Application.Repositories.Product;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories;

public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}