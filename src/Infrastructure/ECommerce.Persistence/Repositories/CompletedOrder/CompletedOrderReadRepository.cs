using ECommerce.Application.Repositories.CompletedOrder;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.CompletedOrder;

public class CompletedOrderReadRepository : ReadRepository<Domain.Entities.CompletedOrder>, ICompletedOrderReadRepository
{
    public CompletedOrderReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}