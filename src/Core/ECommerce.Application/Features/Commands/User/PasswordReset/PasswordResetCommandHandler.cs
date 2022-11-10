using ECommerce.Application.Abstractions.Services;
using MediatR;

namespace ECommerce.Application.Features.Commands.User.PasswordReset;

public class PasswordResetCommandHandler:IRequestHandler<PasswordResetCommandRequest,PasswordResetCommandResponse>
{
    private readonly IAuthService _authService;

    public PasswordResetCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
    {
        await _authService.PasswordResetAsync(request.Email);
        return new();
    }
}