using Microsoft.AspNetCore.Mvc;
using App.Api.Models.Permission;
using App.Application.Interfaces;
using System.Threading.Tasks;

[Route("api/permission")]
[ApiController]
public class PermissionController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPermissions()
    {
        var permissions = await _permissionService.GetPermissionsGroupedByCategoryAsync();
        return Ok(permissions);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionRequest request)
    {
        await _permissionService.CreatePermissionAsync(request.Name, request.Description);
        return Ok(new { Message = "Permission created successfully" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePermission(int id, [FromBody] UpdatePermissionRequest request)
    {
        await _permissionService.UpdatePermissionAsync(id, request.Name, request.Description);
        return Ok(new { Message = "Permission updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePermission(int id)
    {
        await _permissionService.DeletePermissionAsync(id);
        return Ok(new { Message = "Permission deleted successfully" });
    }
}
