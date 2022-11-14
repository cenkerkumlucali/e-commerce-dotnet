using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.DTOs.Role;
using ECommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Persistence.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;

    public RoleService(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public ListRole GetAllRoles(int page, int size)
    {
        IQueryable<Role>? query = _roleManager.Roles;
        List<Role>? result;
        if (page == -1 && size == -1)
            result = query.ToList();
        else
            result = query.Skip(page * size).Take(size).ToList();
        return new ListRole
        {
            Roles = result,
            TotalRoleCount = query.Count()
        };
    }

    public async Task<(string id, string name)> GetRoleById(string id)
    {
        string role = await _roleManager.GetRoleIdAsync(new() { Id = id });
        return (id, role);
    }

    public async Task<bool> CreateRole(string name)
    {
        IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });
        return result.Succeeded;
    }

    public async Task<bool> DeleteRole(string id)
    {
        Role? role = _roleManager.Roles.FirstOrDefault(d => d.Id == id);
        IdentityResult result = await _roleManager.DeleteAsync(role);
        return result.Succeeded;
    }

    public async Task<bool> UpdateRole(string id, string name)
    {
        Role? role = _roleManager.Roles.FirstOrDefault(d => d.Id == id);
        if (role is not null)
        {
            role.Name = name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }

        return false;
    }
}