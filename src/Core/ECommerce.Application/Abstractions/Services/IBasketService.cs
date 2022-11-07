using ECommerce.Application.ViewModels.Baskets;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Abstractions.Services;

public interface IBasketService
{
    Task<List<BasketItem>> GetBasketItemsAsync();
    Task AddItemToBasketAsync(CreateBasketItem basketItem);
    Task UpdateQuantityAsync(UpdateBasketItem basketItem);
    Task RemoveBasketItemAsync(string basketItemId);
}