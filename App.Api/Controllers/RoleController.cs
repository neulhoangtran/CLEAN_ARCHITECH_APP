using Microsoft.AspNetCore.Mvc;
using App.Application.Interfaces;
using App.Api.Models.Role;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[Route("api/role")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = _roleService.GetAllRoles();
        return Ok(roles);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        await _roleService.CreateRoleAsync(request.Name);
        return Ok(new { Message = "Role created successfully" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleRequest request)
    {
        await _roleService.UpdateRoleAsync(id, request.Name);
        return Ok(new { Message = "Role updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        await _roleService.DeleteRoleAsync(id);
        return Ok(new { Message = "Role deleted successfully" });
    }
}
