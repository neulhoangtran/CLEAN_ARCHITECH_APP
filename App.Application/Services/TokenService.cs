using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using App.Application.Interfaces;
using App.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace App.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            // Tạo token logic
            // Kiểm tra nếu UserProfile null thì có thể xử lý thêm ở đây
            var userProfile = user.UserProfile;

            // Tạo các claims (yêu cầu) chứa thông tin người dùng
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim("EmployeeId", user.EmployeeId), // Thêm EmployeeId
            //new Claim(ClaimTypes.Role, user.Role),    // Thêm Role
            new Claim("IpAddress", userProfile?.IPAddress ?? "N/A"), // Thêm IpAddress, kiểm tra null
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            // Khóa bí mật và thông tin ký tên
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Tạo JWT token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        // Triển khai phương thức tạo Access Token
        public string GenerateAccessToken(User user)
        {
            var userProfile = user.UserProfile;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("EmployeeId", user.EmployeeId),
                new Claim("IpAddress", userProfile?.IPAddress ?? "N/A"), // Thêm IpAddress, kiểm tra null
                //new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Triển khai phương thức tạo Refresh Token
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<bool> IsTokenValid(string token)
        {
            // Kiểm tra token có hợp lệ không
            return await Task.FromResult(true);
        }

        // Phương thức lấy UserId từ token, ví dụ cho thấy sử dụng phương thức khác.
        public int GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var userIdClaim = jwtToken?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
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
