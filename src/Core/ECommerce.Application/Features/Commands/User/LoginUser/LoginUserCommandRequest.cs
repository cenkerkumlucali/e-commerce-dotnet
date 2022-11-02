using MediatR;

namespace ECommerce.Application.Features.Commands.User.LoginUser;

public class LoginUserCommandRequest:IRequest<LoginUserCommandResponse>
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }
}