using Microsoft.AspNetCore.Mvc;
using App.Application.Interfaces;
using App.Api.Models.Role;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public IActionResult GetAllRoles()
    {
        var roles = _roleService.GetAllRoles();
        return Ok(roles);
    }

    [HttpPost]
    public IActionResult CreateRole([FromBody] CreateRoleRequest request)
    {
        _roleService.CreateRole(request.Name);
        return Ok(new { Message = "Role created successfully" });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRole(int id, [FromBody] UpdateRoleRequest request)
    {
        _roleService.UpdateRole(id, request.Name);
        return Ok(new { Message = "Role updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRole(int id)
    {
        _roleService.DeleteRole(id);
        return Ok(new { Message = "Role deleted successfully" });
    }
}
