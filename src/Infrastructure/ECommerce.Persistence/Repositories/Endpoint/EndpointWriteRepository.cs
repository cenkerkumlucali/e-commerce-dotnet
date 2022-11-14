using ECommerce.Application.Repositories.Endpoint;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.Endpoint;

public class EndpointWriteRepository : WriteRepository<Domain.Entities.Endpoint>, IEndpointWriteRepository
{
    public EndpointWriteRepository(ECommerceDbContext context) : base(context)
    {
    }
}