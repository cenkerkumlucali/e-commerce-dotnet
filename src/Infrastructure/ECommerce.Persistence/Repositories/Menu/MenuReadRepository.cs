using ECommerce.Application.Repositories.Menu;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.Menu;

public class MenuReadRepository : ReadRepository<Domain.Entities.Menu>, IMenuReadRepository
{
    public MenuReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}