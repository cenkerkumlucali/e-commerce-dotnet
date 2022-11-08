using System.Globalization;
using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.DTOs.Order;
using ECommerce.Application.Repositories.Order;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Services;

public class OrderService : IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IOrderReadRepository _orderReadRepository;

    public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
    {
        _orderWriteRepository = orderWriteRepository;
        _orderReadRepository = orderReadRepository;
    }

    public async Task CreateOrderAsync(CreateOrder createOrder)
    {
        string orderCode = (new Random().NextDouble() * 10000).ToString(CultureInfo.InvariantCulture);
        orderCode = orderCode.Substring(orderCode.IndexOf(".", StringComparison.Ordinal) + 1,
            orderCode.Length - orderCode.IndexOf(".", StringComparison.Ordinal) - 1);
        await _orderWriteRepository.AddAsync(new Order
        {
            Address = createOrder.Address,
            Id = Guid.Parse(createOrder.BasketId),
            Description = createOrder.Description,
            OrderCode = orderCode
        });
        await _orderWriteRepository.SaveAsync();
    }

    public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
    {
        var query = _orderReadRepository.Table.Include(o => o.Basket)
            .ThenInclude(b => b.User)
            .Include(o => o.Basket)
            .ThenInclude(b => b.BasketItems)
            .ThenInclude(bi => bi.Product);

        var data = query.Skip(page * size).Take(size);
        /*.Take((page * size)..size);*/

        return new()
        {
            TotalOrderCount = await query.CountAsync(),
            Orders = await data.Select(o => new
            {
                Id = o.Id,
                CreatedDate = o.CreatedDate,
                OrderCode = o.OrderCode,
                TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                UserName = o.Basket.User.UserName
            }).ToListAsync()
        };
    }

    public async Task<SingleOrder> GetOrderById(string id)
    {
        Order? data = await _orderReadRepository.Table
            .Include(c => c.Basket)
            .ThenInclude(c => c.BasketItems)
            .ThenInclude(c => c.Product).FirstOrDefaultAsync(c => c.Id == Guid.Parse(id));
        return new()
        {
            Id = data?.Id.ToString(),
            BasketItems = data?.Basket.BasketItems.Select(c => new
            {
                c.Product.Name,
                c.Product.Price,
                c.Quantity
            }),
            Address = data.Address,
            CreatedDate = data.CreatedDate,
            Description = data.Description,
            OrderCode = data.OrderCode
        };
    }
}