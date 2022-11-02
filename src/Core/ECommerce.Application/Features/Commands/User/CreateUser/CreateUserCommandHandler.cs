using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.DTOs.User;
using MediatR;

namespace ECommerce.Application.Features.Commands.User.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        CreateUserResponse response = await _userService.CreateAsync(new()
        {
            Email = request.Email,
            NameSurname = request.NameSurname,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
            UserName= request.UserName,
        });

        return new()
        {
            Message = response.Message,
            Succeeded = response.Succeeded,
        };
    }
}