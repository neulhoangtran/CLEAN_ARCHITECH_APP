using System;
using App.Application.Interfaces;
using App.Domain.Entities;

namespace App.Application.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(User user)
        {
            // Tạo token logic
            return "generated_token";
        }

        public async Task<bool> IsTokenValid(string token)
        {
            // Kiểm tra token có hợp lệ không
            return await Task.FromResult(true);
        }

        public int GetUserIdFromToken(string token)
        {
            // Giả lập trích xuất userId từ token
            // Đảm bảo rằng phương thức này trả về kiểu int
            // Ví dụ chuyển đổi chuỗi token thành số nguyên
            // Thực tế nên sử dụng JWT hoặc giải mã token để lấy userId

            // Giả lập nếu token có thể là chuỗi chứa số nguyên
            if (int.TryParse(token, out int userId))
            {
                return userId;
            }

            // Nếu không trích xuất được userId, có thể ném ra ngoại lệ hoặc xử lý phù hợp
            throw new ArgumentException("Invalid token");
        }

        public async Task RevokeToken(string token)
        {
            // Logic thu hồi token
            await Task.CompletedTask;
        }

        public string GeneratePasswordResetToken(User user)
        {
            // Tạo token đặt lại mật khẩu
            return "reset_token";
        }

        public int ValidatePasswordResetToken(string resetToken)
        {
            // Xác thực token đặt lại mật khẩu
            // Giả lập chuyển đổi chuỗi token thành số nguyên
            if (int.TryParse(resetToken, out int userId))
            {
                return userId;
            }

            // Nếu không trích xuất được userId, có thể ném ra ngoại lệ hoặc xử lý phù hợp
            throw new ArgumentException("Invalid reset token");
        }
    }
}
