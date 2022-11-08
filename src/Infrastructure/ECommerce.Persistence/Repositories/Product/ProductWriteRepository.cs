using ECommerce.Application.Repositories;
using ECommerce.Application.Repositories.Product;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories;

public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
    public ProductWriteRepository(ECommerceDbContext context) : base(context)
    {
    }
}