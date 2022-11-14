using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.DTOs.Role;
using MediatR;

namespace ECommerce.Application.Features.Queries.Role.GetRoles;

public class GetRolesQueryHandler:IRequestHandler<GetRolesQueryRequest,GetRolesQueryResponse>
{
    private readonly IRoleService _roleService;

    public GetRolesQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<GetRolesQueryResponse> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
    {
        ListRole roles = _roleService.GetAllRoles(request.Page,request.Size);
        return new()
        {
            Datas = roles.Roles,
            TotalRoleCount = roles.TotalRoleCount
        };
    }
}