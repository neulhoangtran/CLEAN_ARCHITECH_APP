using System;
using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Application.DTOs;
using App.Domain.Repositories;
using App.Domain.Entities;

namespace App.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;  // Repository để tương tác với dữ liệu người dùng
        private readonly ITokenService _tokenService;      // Service để tạo và xác thực token
        private readonly IEmailService _emailService;      // Service để gửi email

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IEmailService emailService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task<AuthTokenDto> Login(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var token = _tokenService.GenerateToken(user);
            return new AuthTokenDto { Token = token, Expiration = DateTime.UtcNow.AddHours(1) };
        }

        public async Task Logout(string token)
        {
            await _tokenService.RevokeToken(token); // Xóa token khỏi danh sách hợp lệ
        }

        public async Task<bool> ValidateToken(string token)
        {
            return await _tokenService.IsTokenValid(token);
        }

        public async Task<UserDto> GetUserFromToken(string token)
        {
            var userId = _tokenService.GetUserIdFromToken(token);
            var user = await _userRepository.GetByIdAsync(userId);

            return new UserDto
            {
                ID = user.ID,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task ChangePassword(int userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || !VerifyPassword(currentPassword, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid current password");
            }

            user.UpdatePassword(HashPassword(newPassword));
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task ForgotPassword(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var resetToken = _tokenService.GeneratePasswordResetToken(user);
            await _emailService.SendPasswordResetEmail(user.Email, resetToken);
        }

        public async Task ResetPassword(string resetToken, string newPassword)
        {
            var userId = _tokenService.ValidatePasswordResetToken(resetToken);
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Invalid reset token");
            }

            user.UpdatePassword(HashPassword(newPassword));
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }

        // Các hàm phụ trợ để xác thực và hash mật khẩu
        private bool VerifyPassword(string password, string passwordHash)
        {
            // So sánh mật khẩu với hash (giả lập)
            return password == passwordHash;
        }

        private string HashPassword(string password)
        {
            // Hàm hash mật khẩu (giả lập)
            return password;
        }
    }
}
