using ECommerce.Application.Repositories;
using ECommerce.Application.Repositories.Order;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Contexts;

namespace ECommerce.Persistence.Repositories;

public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
{
    public OrderWriteRepository(ECommerceDbContext context) : base(context)
    {
    }
}