using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.Entities.Identity;

public class User : IdentityUser<string>
{
    public string NameSurname { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
    
    public ICollection<Basket> Baskets { get; set; }
}