namespace ECommerce.Application.DTOs.Role;

public class ListRole
{
    public List<Domain.Entities.Identity.Role> Roles { get; set; }
    public int TotalRoleCount { get; set; }
}