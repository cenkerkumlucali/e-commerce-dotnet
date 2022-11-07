using ECommerce.Application.Repositories.BasketItem;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.BasketItem;

public class BasketItemWriteRepository : WriteRepository<Domain.Entities.BasketItem>, IBasketItemWriteRepository
{
    public BasketItemWriteRepository(ECommerceDbContext context) : base(context)
    {
    }
}