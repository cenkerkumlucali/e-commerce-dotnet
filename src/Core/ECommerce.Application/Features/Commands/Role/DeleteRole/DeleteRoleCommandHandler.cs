using ECommerce.Application.Abstractions.Services;
using MediatR;

namespace ECommerce.Application.Features.Commands.Role.DeleteRole;

public class DeleteRoleCommandHandler:IRequestHandler<DeleteRoleCommandRequest,DeleteRoleCommandResponse>
{
    private readonly IRoleService _roleService;

    public DeleteRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
    {
        bool result = await _roleService.DeleteRole(request.Id);
        return new() { Succeded = result };
    }
}