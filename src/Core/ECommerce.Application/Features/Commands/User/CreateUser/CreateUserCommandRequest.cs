using MediatR;

namespace ECommerce.Application.Features.Commands.User.CreateUser;

public class CreateUserCommandRequest: IRequest<CreateUserCommandResponse>
{
    public string NameSurname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}