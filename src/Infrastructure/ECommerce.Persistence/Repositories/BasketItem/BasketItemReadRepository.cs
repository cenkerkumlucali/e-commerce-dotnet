using ECommerce.Application.Repositories.BasketItem;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.BasketItem;

public class BasketItemReadRepository : ReadRepository<Domain.Entities.BasketItem>, IBasketItemReadRepository
{
    public BasketItemReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}