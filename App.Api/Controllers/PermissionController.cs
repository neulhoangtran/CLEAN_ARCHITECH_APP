using Microsoft.AspNetCore.Mvc;
using App.Api.Models.Permission;
using App.Application.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class PermissionController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet]
    public IActionResult GetAllPermissions()
    {
        var permissions = _permissionService.GetAllPermissions();
        return Ok(permissions);
    }

    [HttpPost]
    public IActionResult CreatePermission([FromBody] CreatePermissionRequest request)
    {
        _permissionService.CreatePermission(request.Name, request.Description);
        return Ok(new { Message = "Permission created successfully" });
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePermission(int id, [FromBody] UpdatePermissionRequest request)
    {
        _permissionService.UpdatePermission(id, request.Name, request.Description);
        return Ok(new { Message = "Permission updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePermission(int id)
    {
        _permissionService.DeletePermission(id);
        return Ok(new { Message = "Permission deleted successfully" });
    }
}
