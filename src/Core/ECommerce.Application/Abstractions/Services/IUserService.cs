using ECommerce.Application.DTOs.User;

namespace ECommerce.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponse> CreateAsync(CreateUser createUser);
}