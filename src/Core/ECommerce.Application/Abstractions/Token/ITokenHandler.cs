using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Application.Abstractions.Token;

public interface ITokenHandler
{
    DTOs.Token CreateAccessToken(int second, AppUser user);
}