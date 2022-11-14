using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.Abstractions.Services.Configurations;
using ECommerce.Application.Repositories.Endpoint;
using ECommerce.Application.Repositories.Menu;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Action = ECommerce.Application.DTOs.Configuration.Action;

namespace ECommerce.Persistence.Services;

public class AuthorizationEndpointService : IAuthorizationEndpointService
{
    private readonly IApplicationService _applicationService;
    private readonly IEndpointReadRepository _endpointReadRepository;
    private readonly IEndpointWriteRepository _endpointWriteRepository;
    private readonly IMenuReadRepository _menuReadRepository;
    private readonly IMenuWriteRepository _menuWriteRepository;
    private readonly RoleManager<Role> _roleManager;

    public AuthorizationEndpointService(
        IApplicationService applicationService,
        IEndpointReadRepository endpointReadRepository,
        IEndpointWriteRepository endpointWriteRepository,
        IMenuReadRepository menuReadRepository,
        IMenuWriteRepository menuWriteRepository,
        RoleManager<Role> roleManager)
    {
        _applicationService = applicationService;
        _endpointReadRepository = endpointReadRepository;
        _endpointWriteRepository = endpointWriteRepository;
        _menuReadRepository = menuReadRepository;
        _menuWriteRepository = menuWriteRepository;
        _roleManager = roleManager;
    }

    public async Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type)
    {
        Menu _menu = await _menuReadRepository.GetSingleAsync(c => c.Name == menu);
        if (_menu is null)
        {
            _menu = new()
            {
                Id = Guid.NewGuid(),
                Name = menu
            };
            await _menuWriteRepository.AddAsync(_menu);
            await _menuWriteRepository.SaveAsync();
        }

        Endpoint? endpoint =
            await _endpointReadRepository.Table
                .Include(c => c.Menu)
                .Include(c => c.Roles)
                .FirstOrDefaultAsync(c => c.Code == code && c.Menu.Name == menu);
        if (endpoint is null)
        {
            Action? action = _applicationService.GetAuthorizeDefinitionEndpoints(type)
                .FirstOrDefault(c => c.Name == menu)
                ?.Actions.FirstOrDefault(c => c.Code == code);
            endpoint = new()
            {
                Id = Guid.NewGuid(),
                Code = action?.Code,
                ActionType = action?.ActionType,
                HttpType = action?.HttpType,
                Definiton = action?.Definition,
                Menu = _menu
            };
            await _endpointWriteRepository.AddAsync(endpoint);
            await _endpointWriteRepository.SaveAsync();
        }

        foreach (Role? role in endpoint.Roles)
            endpoint.Roles.Remove(role);

        List<Role> appRole = await _roleManager.Roles.Where(c => roles.Contains(c.Name)).ToListAsync();
        foreach (var role in appRole)
            endpoint.Roles.Add(role);
        await _endpointWriteRepository.SaveAsync();
    }

    public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
    {
        Endpoint? endpoint = await _endpointReadRepository.Table
            .Include(c => c.Roles)
            .Include(c=>c.Menu)
            .FirstOrDefaultAsync(c => c.Code == code && c.Menu.Name == menu);
        return endpoint?.Roles.Select(c => c.Name).ToList();
    }
}