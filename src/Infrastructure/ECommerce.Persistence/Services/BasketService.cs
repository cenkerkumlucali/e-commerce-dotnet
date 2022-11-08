using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.DTOs.Baskets;
using ECommerce.Application.Repositories.Basket;
using ECommerce.Application.Repositories.BasketItem;
using ECommerce.Application.Repositories.Order;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Services;

public class BasketService : IBasketService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User?> _userManager;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IBasketWriteRepository _basketWriteRepository;
    private readonly IBasketReadRepository _basketReadRepository;
    private readonly IBasketItemWriteRepository _basketItemWriteRepository;
    private readonly IBasketItemReadRepository _basketItemReadRepository;

    public BasketService(
        IHttpContextAccessor httpContextAccessor,
        UserManager<User?> userManager, IOrderReadRepository orderReadRepository,
        IBasketWriteRepository basketWriteRepository,
        IBasketItemWriteRepository basketItemWriteRepository,
        IBasketItemReadRepository basketItemReadRepository,
        IBasketReadRepository basketReadRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _orderReadRepository = orderReadRepository;
        _basketWriteRepository = basketWriteRepository;
        _basketItemWriteRepository = basketItemWriteRepository;
        _basketItemReadRepository = basketItemReadRepository;
        _basketReadRepository = basketReadRepository;
    }

    private async Task<Basket> ContextUser()
    {
        var username = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        if (!string.IsNullOrEmpty(username))
        {
            User? user = await _userManager.Users
                .Include<User, ICollection<Basket>>(c => c.Baskets)
                .FirstOrDefaultAsync(c => c.UserName == username);
            var _basket = from basket in user.Baskets
                join order in _orderReadRepository.Table
                    on basket.Id equals order.Id into BasketOrders
                from order in BasketOrders.DefaultIfEmpty()
                select new
                {
                    Basket = basket,
                    Order = order
                };
            Basket? targetBasket = null;
            if (_basket.Any(c => c.Order is null))
                targetBasket = _basket.FirstOrDefault(c => c.Order is null)?.Basket;
            else
            {
                targetBasket = new();
                user.Baskets.Add(targetBasket);
            }

            await _basketWriteRepository.SaveAsync();
            return targetBasket;
        }

        throw new Exception("Beklenmeyen bir hata ile karşılaşıldı");
    }

    public async Task<List<BasketItem>> GetBasketItemsAsync()
    {
        Basket basket = await ContextUser();
        Basket? result = _basketReadRepository.Table
            .Include(c => c.BasketItems)
            .ThenInclude(c => c.Product)
            .FirstOrDefault(c => c.Id == basket.Id);
        return result.BasketItems.ToList();
    }

    public async Task AddItemToBasketAsync(CreateBasketItem basketItem)
    {
        Basket basket = await ContextUser();
        if (basket is not null)
        {
            BasketItem item = await _basketItemReadRepository.GetSingleAsync(c =>
                c.BasketId == basket.Id && c.ProductId == Guid.Parse(basketItem.ProductId));
            if (item is not null)
                item.Quantity++;
            else
                await _basketItemWriteRepository.AddAsync(new()
                {
                    BasketId = basket.Id,
                    ProductId = Guid.Parse(basketItem.ProductId),
                    Quantity = basketItem.Quantity
                });
            await _basketItemWriteRepository.SaveAsync();
        }
    }

    public async Task UpdateQuantityAsync(UpdateBasketItem basketItem)
    {
        BasketItem? item = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
        if (item is not null)
        {
            item.Quantity = basketItem.Quantity;
            await _basketItemWriteRepository.SaveAsync();
        }
    }

    public async Task RemoveBasketItemAsync(string basketItemId)
    {
        BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
        if (basketItem is not null)
        {
            _basketItemWriteRepository.Remove(basketItem);
            await _basketItemWriteRepository.SaveAsync();
        }
    }

    public Basket? GetUserActiveBasket => ContextUser().Result;
}