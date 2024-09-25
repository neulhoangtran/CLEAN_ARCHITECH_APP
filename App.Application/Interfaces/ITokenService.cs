using App.Domain.Entities;

namespace App.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        Task<bool> IsTokenValid(string token);
        int GetUserIdFromToken(string token);
        Task RevokeToken(string token);
        string GeneratePasswordResetToken(User user);
        int ValidatePasswordResetToken(string resetToken);
    }
}