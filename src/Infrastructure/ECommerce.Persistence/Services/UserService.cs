using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.DTOs.User;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Helpers;
using ECommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserResponse> CreateAsync(CreateUser createUser)
    {
        IdentityResult result = await _userManager.CreateAsync(new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = createUser.UserName,
            Email = createUser.Email,
            NameSurname = createUser.NameSurname,
        }, createUser.Password);

        CreateUserResponse response = new() { Succeeded = result.Succeeded };

        if (result.Succeeded)
            response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}\n";

        return response;
    }

    public async Task UpdateRefreshTokenAsync(string refreshToken, User user, DateTime accessTokenDate,
        int addOnAccessTokenDate)
    {
        if (user is not null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
            await _userManager.UpdateAsync(user);
        }
        else
            throw new NotFoundUserException();
    }

    public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
    {
        User user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            resetToken = resetToken.UrlDecode();
            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (result.Succeeded)
                await _userManager.UpdateSecurityStampAsync(user);
            else
                throw new PasswordChangeFailedException();
        }
    }
}