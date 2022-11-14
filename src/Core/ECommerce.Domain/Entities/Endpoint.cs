using ECommerce.Domain.Entities.Common;
using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Domain.Entities;

public class Endpoint : BaseEntity
{
    public Endpoint()
    {
        Roles = new HashSet<Role>();
    }
    public string ActionType { get; set; }
    public string HttpType { get; set; }
    public string Definiton { get; set; }
    public string Code { get; set; }
    public Menu Menu { get; set; }
    public ICollection<Role> Roles { get; set; }
}