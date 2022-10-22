using ECommerce.Application.Repositories.File;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.File;

public class FileWriteRepository:WriteRepository<Domain.Entities.File>,IFileWriteRepository
{
    public FileWriteRepository(ECommerceDbContext context) : base(context)
    {
    }
}