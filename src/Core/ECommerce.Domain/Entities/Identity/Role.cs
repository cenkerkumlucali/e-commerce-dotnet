using Microsoft.AspNetCore.Identity;

namespace ECommerce.Domain.Entities.Identity;

public class Role : IdentityRole<string>
{
    public ICollection<Endpoint> EndPoints { get; set; } 
}