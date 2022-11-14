using MediatR;

namespace ECommerce.Application.Features.Queries.Role.GetRoleById;

public class GetRoleByIdQueryRequest:IRequest<GetRoleByIdQueryResponse>
{
    public string Id { get; set; }
}