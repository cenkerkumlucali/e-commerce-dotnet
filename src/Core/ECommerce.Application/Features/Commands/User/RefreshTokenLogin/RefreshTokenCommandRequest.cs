using MediatR;

namespace ECommerce.Application.Features.Commands.User.RefreshTokenLogin;

public class RefreshTokenCommandRequest:IRequest<RefreshTokenCommandResponse>
{
    public string RefreshToken { get; set; }
}