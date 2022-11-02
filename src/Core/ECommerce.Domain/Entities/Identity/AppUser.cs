using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.Entities.Identity;

public class AppUser : IdentityUser<string>
{
    public string NameSurname { get; set; }
}