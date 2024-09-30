using App.Application.DTOs;

namespace App.Application.Interfaces
{
    public interface IAuthService
    {
        // Đăng nhập người dùng
        Task<AuthTokenDto> LoginAsync(string username, string password);

        Task RegisterUserAsync(UserDto userDto);

        // Đăng xuất người dùng
        Task Logout(string token);    
    }
}
