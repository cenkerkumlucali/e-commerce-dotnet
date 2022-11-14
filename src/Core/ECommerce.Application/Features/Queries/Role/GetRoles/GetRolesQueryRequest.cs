using MediatR;

namespace ECommerce.Application.Features.Queries.Role.GetRoles;

public class GetRolesQueryRequest:IRequest<GetRolesQueryResponse>
{
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 5;
}