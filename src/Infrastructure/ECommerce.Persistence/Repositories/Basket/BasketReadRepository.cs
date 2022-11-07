using ECommerce.Application.Repositories.Basket;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.Basket;

public class BasketReadRepository : ReadRepository<Domain.Entities.Basket>, IBasketReadRepository
{
    public BasketReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}