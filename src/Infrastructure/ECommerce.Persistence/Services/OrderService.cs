using System.Globalization;
using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.DTOs.Order;
using ECommerce.Application.Repositories.CompletedOrder;
using ECommerce.Application.Repositories.Order;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ECommerce.Persistence.Services;

public class OrderService : IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
    private readonly ICompletedOrderReadRepository _completedOrderReadRepository;

    public OrderService(
        IOrderWriteRepository orderWriteRepository,
        IOrderReadRepository orderReadRepository,
        ICompletedOrderWriteRepository completedOrderWriteRepository,
        ICompletedOrderReadRepository completedOrderReadRepository)
    {
        _orderWriteRepository = orderWriteRepository;
        _orderReadRepository = orderReadRepository;
        _completedOrderWriteRepository = completedOrderWriteRepository;
        _completedOrderReadRepository = completedOrderReadRepository;
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
        IIncludableQueryable<Order, Product> query = _orderReadRepository.Table.Include(o => o.Basket)
            .ThenInclude(b => b.User)
            .Include(o => o.Basket)
            .ThenInclude(b => b.BasketItems)
            .ThenInclude(bi => bi.Product);

        IQueryable<Order> data = query.Skip(page * size).Take(size);

        var data2 = from order in data
            join completedOrder in _completedOrderReadRepository.Table
                on order.Id equals completedOrder.OrderId into co
            from _co in co.DefaultIfEmpty()
            select new
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                OrderCode = order.OrderCode,
                Basket = order.Basket,
                Completed = _co != null ? true : false
            };

        return new ListOrder
        {
            TotalOrderCount = await query.CountAsync(),
            Orders = await data2.Select(o => new
            {
                Id = o.Id,
                CreatedDate = o.CreatedDate,
                OrderCode = o.OrderCode,
                TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                UserName = o.Basket.User.UserName,
                o.Completed
            }).ToListAsync()
        };
    }

    public async Task<SingleOrder> GetOrderByIdAsync(string id)
    {
        IIncludableQueryable<Order, Product>? data = _orderReadRepository.Table
            .Include(c => c.Basket)
            .ThenInclude(c => c.BasketItems)
            .ThenInclude(c => c.Product);
        
        var data2 = await (from order in data
            join completedOrder in _completedOrderReadRepository.Table
                on order.Id equals completedOrder.OrderId into co
            from _co in co.DefaultIfEmpty()
            select new
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                OrderCode = order.OrderCode,
                Basket = order.Basket,
                Completed = _co != null ? true : false,
                Address=order.Address,
                Description = order.Description
            }).FirstOrDefaultAsync(c => c.Id == Guid.Parse(id));;
            
        
        return new()
        {
            Id = data2?.Id.ToString(),
            BasketItems = data2?.Basket.BasketItems.Select(c => new
            {
                c.Product.Name,
                c.Product.Price,
                c.Quantity
            }),
            Address = data2.Address,
            CreatedDate = data2.CreatedDate,
            Description = data2.Description,
            OrderCode = data2.OrderCode,
            Completed = data2.Completed
        };
    }

    public async Task<(bool,CompletedOrderDto)> CompleteOrderAsync(string id)
    {
        Order? order = await _orderReadRepository.Table
            .Include(c=>c.Basket)
            .ThenInclude(c=>c.User)
            .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
        if (order is not null)
        {
            await _completedOrderWriteRepository.AddAsync(new(){ OrderId = Guid.Parse(id) });
            return (await _completedOrderWriteRepository.SaveAsync() > 0,new()
            {
                OrderCode = order.OrderCode,
                OrderDate = order.CreatedDate,
                Username = order.Basket.User.UserName,
                EMail = order.Basket.User.Email
            });
        }
        return (false, null);
    }
}