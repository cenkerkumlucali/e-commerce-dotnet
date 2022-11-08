using ECommerce.Application.Repositories;
using ECommerce.Application.Repositories.Customer;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories;

public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
{
    public CustomerReadRepository(ECommerceDbContext context) : base(context)
    {
    }
}