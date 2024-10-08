﻿using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using App.Domain.Repositories;
using App.Application.DTOs;

namespace App.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;  // Repository để tương tác với dữ liệu người dùng
        public TokenService(IConfiguration configuration, ITokenRepository tokenRepository, IUserRepository userRepository)
        {
            _configuration = configuration;
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }

        // Tạo Access Token cho người dùng
        public string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("EmployeeId", user.EmployeeId), // Thêm thông tin EmployeeId vào claims
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
                expires: DateTime.Now.AddMinutes(30), // Thời gian sống của Access Token là 30 phút
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Tạo Refresh Token ngẫu nhiên
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        // Kiểm tra tính hợp lệ của Access Token
        public async Task<bool> IsTokenValid(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return await Task.FromResult(principal != null);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        // Lấy thông tin UserId từ Access Token
        public int GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var userIdClaim = jwtToken?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

        // Thu hồi (revoke) token
        public async Task RevokeToken(string token)
        {
            var tokenEntity = await _tokenRepository.GetByTokenAsync(token);
            if (tokenEntity != null)
            {
                await _tokenRepository.RemoveTokenAsync(tokenEntity);
            }
        }

        // Tạo token đặt lại mật khẩu
        public string GeneratePasswordResetToken(User user)
        {
            // Tạo một token đặt lại mật khẩu, ví dụ sử dụng một hash hoặc mã hóa thông tin người dùng
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes($"{user.Username}{DateTime.UtcNow}"));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // Xác thực token đặt lại mật khẩu
        public int ValidatePasswordResetToken(string resetToken)
        {
            // Ở đây bạn có thể so sánh với token đã lưu trong cơ sở dữ liệu
            // Ví dụ: Trích xuất UserId từ token đã lưu trong DB nếu token hợp lệ
            if (int.TryParse(resetToken, out int userId))
            {
                return userId;
            }

            // Nếu không trích xuất được userId, có thể ném ra ngoại lệ hoặc xử lý phù hợp
            throw new ArgumentException("Invalid reset token");
        }
        public async Task<int> GetUserTokenCountAsync(int userId)
        {
            return await _tokenRepository.GetUserTokenCountAsync(userId);
        }

        public async Task<UserDto> GetUserFromToken(string token)
        {
            var userId = GetUserIdFromToken(token);
            var user = await _userRepository.GetByIdAsync(userId);

            return new UserDto
            {
                ID = user.ID,
                Username = user.Username,
                Email = user.Email,
            };
        }

        public async Task<AuthTokenDto> RefreshAccessTokenAsync(string refreshToken)
        {
            // Kiểm tra refresh token trong cơ sở dữ liệu
            var storedToken = await _tokenRepository.GetByTokenAsync(refreshToken);
            if (storedToken == null || storedToken.Expiration < DateTime.UtcNow)
            {
                return null; // Token không hợp lệ hoặc đã hết hạn
            }

            // Lấy thông tin người dùng
            var user = await _userRepository.GetByIdAsync(storedToken.UserID);
            if (user == null)
            {
                return null; // Người dùng không tồn tại
            }

            // Xóa các token cũ nếu vượt quá 3 token
            var userTokens = await _tokenRepository.GetTokensByUserIdAsync(user.ID);
            if (userTokens.Count >= 3)
            {
                var tokensToRemove = userTokens.OrderBy(t => t.Expiration).Take(userTokens.Count - 2).ToList();
                _tokenRepository.RemoveRange(tokensToRemove);
            }

            // Tạo mới Access Token
            var accessToken = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken();

            // Cập nhật refresh token mới vào cơ sở dữ liệu
            storedToken.TokenValue = newRefreshToken;
            storedToken.Expiration = DateTime.UtcNow.AddDays(2);

            _tokenRepository.Update(storedToken);
            await _tokenRepository.SaveChangesAsync();

            return new AuthTokenDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
