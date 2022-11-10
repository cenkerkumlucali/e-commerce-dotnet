using ECommerce.Application.Repositories.CompletedOrder;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.CompletedOrder;

public class CompletedOrderWriteRepository : WriteRepository<Domain.Entities.CompletedOrder>, ICompletedOrderWriteRepository
{
    public CompletedOrderWriteRepository(ECommerceDbContext context) : base(context)
    {
    }
}