using Microsoft.AspNetCore.Mvc;
using App.Application.Interfaces;
using App.Application.DTOs;
using App.Api.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        // Inject UserService vào Controller
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Đăng ký người dùng mới
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
        {
            try
            {
                var userDto = new UserDto
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = request.Password, // Giả sử password đã được hash
                    Role = request.Role
                };

                _userService.RegisterUser(userDto);
                return Ok(new { Message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Đăng nhập người dùng
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                // Gọi service để xác thực người dùng
                var user = _userService.Login(request.Username, request.Password);
                if (user == null)
                {
                    return Unauthorized(new { Message = "Invalid username or password" });
                }

                return Ok(new { Message = "Login successful", User = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Lấy thông tin người dùng theo Id
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found" });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Cập nhật thông tin người dùng
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                var userDto = new UserDto
                {
                    FullName = request.FullName,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Role = request.Role
                };

                _userService.UpdateUser(id, userDto);
                return Ok(new { Message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Xóa người dùng theo Id
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok(new { Message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
