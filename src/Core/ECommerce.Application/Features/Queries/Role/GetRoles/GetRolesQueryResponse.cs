namespace ECommerce.Application.Features.Queries.Role.GetRoles;

public class GetRolesQueryResponse
{
    public List<Domain.Entities.Identity.Role> Datas { get; set; }
    public int TotalRoleCount { get; set; }
}