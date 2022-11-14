using ECommerce.Application.Abstractions.Services;
using MediatR;

namespace ECommerce.Application.Features.Queries.Role.GetRoleById;

public class GetRoleByIdQueryHandler:IRequestHandler<GetRoleByIdQueryRequest,GetRoleByIdQueryResponse>
{
    private readonly IRoleService _roleService;

    public GetRoleByIdQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
    {
        (string id, string name) result = await _roleService.GetRoleById(request.Id);
        return new GetRoleByIdQueryResponse
        {
            Id = result.id,
            Name = result.name
        };
    }
}