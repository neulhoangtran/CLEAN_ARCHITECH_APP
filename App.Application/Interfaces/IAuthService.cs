using App.Application.DTOs;

namespace App.Application.Interfaces
{
    public interface IAuthService
    {
        // Đăng nhập người dùng
        Task<AuthTokenDto> LoginAsync(string username, string password);

        void RegisterUser(UserDto userDto);

        // Đăng xuất người dùng
        Task Logout(string token);

        // Xác thực token
        Task<bool> ValidateToken(string token);

        // Lấy thông tin người dùng từ token
        Task<UserDto> GetUserFromToken(string token);

        // Đổi mật khẩu
        Task ChangePassword(int userId, string currentPassword, string newPassword);

        // Quên mật khẩu
        Task ForgotPassword(string email);

        // Đặt lại mật khẩu
        Task ResetPassword(string resetToken, string newPassword);

        Task<AuthTokenDto> RefreshAccessTokenAsync(string refreshToken);
    }
}
