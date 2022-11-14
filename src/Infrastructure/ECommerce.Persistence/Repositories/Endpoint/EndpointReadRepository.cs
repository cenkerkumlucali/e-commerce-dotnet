using ECommerce.Application.Repositories.Endpoint;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories.Endpoint;

public class EndpointReadRepository : ReadRepository<Domain.Entities.Endpoint>, IEndpointReadRepository
{
    public EndpointReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}