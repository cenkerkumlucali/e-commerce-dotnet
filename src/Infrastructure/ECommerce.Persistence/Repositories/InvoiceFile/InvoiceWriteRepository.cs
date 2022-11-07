using ECommerce.Application.Repositories.InvoiceFile;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.InvoiceFile;

public class InvoiceWriteRepository : WriteRepository<Domain.Entities.InvoiceFile>, IInvoiceWriteRepository
{
    public InvoiceWriteRepository(ECommerceDbContext context) : base(context)
    {
    }
}