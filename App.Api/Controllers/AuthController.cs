using Microsoft.AspNetCore.Mvc;
using App.Application.Interfaces;   // Sử dụng các interface từ Application layer
using App.Application.DTOs;        // Sử dụng các Data Transfer Object từ Application layer
using ApiLoginRequest = App.Api.Models.LoginRequest; // Alias cho LoginRequest từ App.Api.Models
using App.Api.Models; // Alias cho LoginRequest từ App.Api.Models
using Microsoft.AspNetCore.Identity.Data; // Đảm bảo không dùng tên LoginRequest từ Microsoft.AspNetCore.Identity.Data

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
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
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
        {
            try
            {
                var userDto = new UserDto
                {

                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = request.Password, // Giả sử password đã được hash trong service
                    //Role = request.Role,
                    //PhoneNumber = request.PhoneNumber,
                    //FullName = request.FullName,
                    //Address = request.Address

                };

                _authService.RegisterUser(userDto);
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
                    return Unauthorized(new { Message = "Invalid username or password" });

                if (token is not AuthTokenDto)
                    return Unauthorized(new { Message = "Invalid token, please contact with admin." });

                return Ok(new { Message = "Login successful", token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        
        // API để làm mới Access Token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var newTokens = await _authService.RefreshAccessTokenAsync(request.RefreshToken);
            if (newTokens == null)
            {
                return Unauthorized(new { Message = "Invalid or expired refresh token" });
            }

            return Ok(newTokens);
        }
    }
}
