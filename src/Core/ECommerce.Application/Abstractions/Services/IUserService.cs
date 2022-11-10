using ECommerce.Application.DTOs.User;
using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Application.Abstractions.Services;

public interface IUserService
{
    Task<CreateUserResponse> CreateAsync(CreateUser createUser);
    Task UpdateRefreshTokenAsync(string refreshToken, User user, DateTime accessTokenDate, int addOnAccessTokenDate);
    Task UpdatePasswordAsync(string userId,string resetToken,string newPassword);
}