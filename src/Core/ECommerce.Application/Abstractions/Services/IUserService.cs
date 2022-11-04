using ECommerce.Application.DTOs.User;
using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponse> CreateAsync(CreateUser createUser);
    Task UpdateRefreshToken(string refreshToken, User user, DateTime accessTokenDate, int addOnAccessTokenDate);
}