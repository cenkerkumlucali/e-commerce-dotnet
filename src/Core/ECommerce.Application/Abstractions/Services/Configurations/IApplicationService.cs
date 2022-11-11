using ECommerce.Application.DTOs.Configuration;

namespace ECommerce.Application.Abstractions.Services.Configurations;

public interface IApplicationService
{
    List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
}