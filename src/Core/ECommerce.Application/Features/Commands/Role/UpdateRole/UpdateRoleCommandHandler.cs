using ECommerce.Application.Abstractions.Services;
using MediatR;

namespace ECommerce.Application.Features.Commands.Role.UpdateRole;

public class UpdateRoleCommandHandler:IRequestHandler<UpdateRoleCommandRequest,UpdateRoleCommandResponse>
{
    private readonly IRoleService _roleService;

    public UpdateRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _roleService.UpdateRole(request.Id,request.Name);
        return new() { Succeded = result };
    }
}