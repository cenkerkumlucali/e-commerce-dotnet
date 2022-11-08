using ECommerce.Application.Repositories;
using ECommerce.Application.Repositories.Customer;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories;

public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
{
    public CustomerWriteRepository(ECommerceDbContext context) : base(context)
    {
    }
}