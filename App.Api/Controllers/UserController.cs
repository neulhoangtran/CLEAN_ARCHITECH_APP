using Microsoft.AspNetCore.Mvc;
using App.Application.Interfaces;   // Sử dụng các interface từ Application layer
using App.Application.DTOs;        // Sử dụng các Data Transfer Object từ Application layer
using ApiLoginRequest = App.Api.Models.LoginRequest; // Alias cho LoginRequest từ App.Api.Models
using App.Api.Models; // Alias cho LoginRequest từ App.Api.Models
using Microsoft.AspNetCore.Identity.Data; // Đảm bảo không dùng tên LoginRequest từ Microsoft.AspNetCore.Identity.Data
using Microsoft.AspNetCore.Authorization;
namespace App.Api.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        // Inject UserService vào Controller
        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        // API lấy tất cả người dùng
        [HttpGet("list")]
        public async Task<IActionResult> GetAllUsers(int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                // Lấy danh sách phân trang từ service
                var paginatedUsers = await _userService.GetPaginatedUsersAsync(pageIndex, pageSize);

                if (paginatedUsers == null || paginatedUsers.Items.Count == 0)
                {
                    return NotFound(new { Message = "No users found" });
                }

                return Ok(new
                {
                    Data = paginatedUsers.Items,
                    Pagination = new
                    {
                        PageIndex = paginatedUsers.PageIndex,
                        TotalPages = paginatedUsers.TotalPages,
                        HasPreviousPage = paginatedUsers.HasPreviousPage,
                        HasNextPage = paginatedUsers.HasNextPage
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }


        // Lấy thông tin người dùng theo Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found" });
                }

                return Ok(new {User = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Cập nhật thông tin người dùng
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                var userDto = new UserDto
                {
                    FullName = request.FullName,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Username = request.Username,
                    Role = request.Role.HasValue ? request.Role.Value : 0 // Sử dụng giá trị mặc định nếu Role là null
                };

                await _userService.UpdateUserAsync(id, userDto);
                return Ok(new { Message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Xóa người dùng theo Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok(new { Message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        
    }
}
