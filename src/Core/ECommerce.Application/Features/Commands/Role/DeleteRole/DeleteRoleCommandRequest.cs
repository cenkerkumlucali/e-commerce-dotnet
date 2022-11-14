using MediatR;

namespace ECommerce.Application.Features.Commands.Role.DeleteRole;

public class DeleteRoleCommandRequest:IRequest<DeleteRoleCommandResponse>
{
    public string Id { get; set; }
}