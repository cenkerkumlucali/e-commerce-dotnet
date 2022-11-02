using ECommerce.Application.DTOs;

namespace ECommerce.Application.Features.Commands.User.GoogleLogin;

public class GoogleLoginCommandResponse
{
    public Token Token { get; set; }
}