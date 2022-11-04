using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.DTOs;
using MediatR;

namespace ECommerce.Application.Features.Commands.User.RefreshTokenLogin;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request,CancellationToken cancellationToken)
    {
        Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
        return new RefreshTokenCommandResponse
        {
            Token = token
        };
    }
}