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
    public async Task<IActionResult> GetAllRoles(int pageIndex = 1, int pageSize = 10)
    {
        try
        {
            // Lấy danh sách các vai trò phân trang từ service
            var paginatedRoles = await _roleService.GetPaginatedRolesAsync(pageIndex, pageSize);

            if (paginatedRoles == null || paginatedRoles.Items.Count == 0)
            {
                return NotFound(new { Message = "No roles found" });
            }

            // Trả về danh sách role cùng với thông tin phân trang
            return Ok(new
            {
                Data = paginatedRoles.Items,
                Pagination = new
                {
                    PageIndex = paginatedRoles.PageIndex,
                    TotalPages = paginatedRoles.TotalPages,
                    HasPreviousPage = paginatedRoles.HasPreviousPage,
                    HasNextPage = paginatedRoles.HasNextPage
                }
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        await _roleService.CreateRoleAsync(request.Name);
        return Ok(new { Message = "Role created successfully" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(int id)
    {
        try
        {
            // Call the service to get the role by ID
            var role = await _roleService.GetRoleById(id);

            // If no role is found, return a 404 response
            if (role == null)
            {
                return NotFound(new { Message = "Role not found" });
            }

            // Return the role information
            return Ok(role);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
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

    [HttpPost("{roleId}/add-user")]
    public async Task<IActionResult> AddUserToRole(int roleId, [FromBody] AddUserToRoleRequest request)
    {
        await _roleService.AddUserToRoleAsync(roleId, request.UserId);
        return Ok(new { Message = "User added to role successfully" });
    }
}
