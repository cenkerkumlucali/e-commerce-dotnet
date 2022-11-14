namespace ECommerce.Application.Abstractions.Services;

public interface IAuthorizationEndpointService
{
    Task AssignRoleEndpointAsync(string[] role, string menu, string code, Type type);
    Task<List<string>> GetRolesToEndpointAsync(string code,string menu);
}