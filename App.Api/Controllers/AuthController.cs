using Microsoft.AspNetCore.Mvc;
using App.Application.Interfaces;   // Sử dụng các interface từ Application layer
using App.Application.DTOs;        // Sử dụng các Data Transfer Object từ Application layer
using ApiLoginRequest = App.Api.Models.LoginRequest; // Alias cho LoginRequest từ App.Api.Models
using App.Api.Models; // Alias cho LoginRequest từ App.Api.Models
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization; // Đảm bảo không dùng tên LoginRequest từ Microsoft.AspNetCore.Identity.Data

namespace App.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        // Inject UserService vào Controller
        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        // Đăng ký người dùng mới
        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            try
            {
                var userDto = new UserDto
                {

                    Username = request.Username,
                    Email = request.Email,
                    EmployeeId = request.EmployeeId,
                    PasswordHash = request.Password, // Giả sử password đã được hash trong service
                    PhoneNumber = request.PhoneNumber, // Giá trị có thể null
                    FullName = request.FullName, // Giá trị có thể null
                    Address = request.Address, // Giá trị có thể null
                    Role = request.Role ?? 2
                    //PhoneNumber = request.PhoneNumber,
                    //FullName = request.FullName,
                    //Address = request.Address

                };

                await _authService.RegisterUserAsync(userDto);
                return Ok(new { Message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Đăng nhập người dùng
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ApiLoginRequest request) // Sử dụng alias ApiLoginRequest để tránh nhầm lẫn
        {
            try
            {
                // Gọi service để xác thực người dùng
                var token = await _authService.LoginAsync(request.Username, request.Password);

                // Kiểm tra nếu user trả về là null
                if (token == null)
                    return Unauthorized(new { message = "Invalid username or password" });

                if (token is not AuthTokenDto)
                    return Unauthorized(new { message = "Invalid token, please contact with admin." });

                return Ok(new { message = "Login successful", Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
