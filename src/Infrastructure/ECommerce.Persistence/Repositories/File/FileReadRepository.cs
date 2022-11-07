using ECommerce.Application.Repositories.File;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.File;

public class FileReadRepository : ReadRepository<Domain.Entities.File>, IFileReadRepository
{
    public FileReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}