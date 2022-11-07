using ECommerce.Application.Repositories.Basket;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.Basket;

public class BasketWriteRepository : WriteRepository<Domain.Entities.Basket>, IBasketWriteRepository
{
    public BasketWriteRepository(ECommerceDbContext context) : base(context)
    {
    }
}