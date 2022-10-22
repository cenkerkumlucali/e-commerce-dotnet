using ECommerce.Application.Repositories.InvoiceFile;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.InvoiceFile;

public class InvoiceReadRepository:ReadRepository<Domain.Entities.InvoiceFile>,IInvoiceReadRepository
{
    public InvoiceReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}