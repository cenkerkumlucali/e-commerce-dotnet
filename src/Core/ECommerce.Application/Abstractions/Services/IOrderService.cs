using ECommerce.Application.DTOs.Order;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Abstractions.Services;

public interface IOrderService
{
    Task CreateOrderAsync(CreateOrder createOrder);
    Task<ListOrder> GetAllOrdersAsync(int page,int size);
    Task<SingleOrder> GetOrderByIdAsync(string id);
    Task<(bool, CompletedOrderDto)> CompleteOrderAsync(string id);
}