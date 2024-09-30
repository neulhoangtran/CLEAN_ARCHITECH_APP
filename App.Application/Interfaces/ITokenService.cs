using App.Application.DTOs;
using App.Domain.Entities;

namespace App.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user); // Phương thức tạo Access Token
        string GenerateRefreshToken(); // Phương thức tạo Refresh Token
        Task<bool> IsTokenValid(string token);
        int GetUserIdFromToken(string token);
        Task RevokeToken(string token);
        Task<int> GetUserTokenCountAsync(int userId);
        Task<AuthTokenDto> RefreshAccessTokenAsync(string refreshToken);
        //string GeneratePasswordResetToken(User user);
        //int ValidatePasswordResetToken(string resetToken);
    }
}