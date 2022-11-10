using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.Exceptions;
using MediatR;

namespace ECommerce.Application.Features.Commands.User.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
{
    private readonly IUserService _userService;

    public UpdatePasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request,
        CancellationToken cancellationToken)
    {
        if (!request.Password.Equals(request.PasswordConfirm))
            throw new PasswordConfirmFailedException();
        await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);
        return new UpdatePasswordCommandResponse();
    }
}