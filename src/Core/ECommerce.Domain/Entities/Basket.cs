using ECommerce.Domain.Entities.Common;
using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Domain.Entities;

public class Basket : BaseEntity
{
    public string UserId { get; set; }
    
    public User User { get; set; }
    public Order Order { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }  
}