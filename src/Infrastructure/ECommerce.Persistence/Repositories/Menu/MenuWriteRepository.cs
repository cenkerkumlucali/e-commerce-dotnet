using ECommerce.Application.Repositories.Menu;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.Menu;

public class MenuWriteRepository : WriteRepository<Domain.Entities.Menu>, IMenuWriteRepository
{
    public MenuWriteRepository(ECommerceDbContext context) : base(context)
    {
    }
}