using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.DTOs.User;
using ECommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserResponse> CreateAsync(CreateUser createUser)
    {
        IdentityResult result = await _userManager.CreateAsync(new AppUser
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
}