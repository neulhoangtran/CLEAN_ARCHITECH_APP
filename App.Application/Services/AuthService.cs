using System;
using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Application.DTOs;
using App.Domain.Repositories;
using App.Domain.Entities;
using App.Domain.Services;
using App.Domain.Events;
using App.Domain.Events.User;
using App.Domain.Services;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;


namespace App.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;  // Repository để tương tác với dữ liệu người dùng
        private readonly ITokenService _tokenService;      // Service để tạo và xác thực token
        private readonly IEmailService _emailService;      // Service để gửi email
        private readonly ITokenRepository _tokenRepository;
        private readonly UserDomainService _userDomainService;
        private readonly IEventBus _eventBus;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IEmailService emailService, ITokenRepository tokenRepository, UserDomainService userDomainService, IEventBus eventBus)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _emailService = emailService;
            _tokenRepository = tokenRepository;
            _userDomainService = userDomainService;
            _eventBus = eventBus;
        }

        public async Task RegisterUserAsync(UserDto userDto)
        {
            bool UniqueUser = await _userDomainService.IsUsernameUnique(userDto.Username);
            if (!UniqueUser)
                throw new Exception("Username already exists");

            var user = new User(
                userDto.Username,
                userDto.EmployeeId,
                userDto.Email,
                HashPassword("Admin@123"),
                userDto.Status,
                userDto.FullName,
                userDto.Address,
                userDto.PhoneNumber,
                userDto.Role
                );
            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            var userRegisteredEvent = new UserRegisteredEvent(user.ID, user.Username, user.Email);
            _eventBus.Publish(userRegisteredEvent);
        }

        // Đăng nhập người dùng
        public async Task<AuthTokenDto> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            // Kiểm tra nếu không phải là user
            if (user is not User)
               return null;

            // Kiểm tra nếu tồn tại user nhưng sai password
            if (user == null || !VerifyPassword(password, user.PasswordHash))
                return null;

            // Tạo Access Token và Refresh Token
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Lưu Refresh Token vào cơ sở dữ liệu
            var tokenEntity = new Token(user.ID, refreshToken, "RefreshToken", DateTime.UtcNow.AddDays(7));
            await _tokenRepository.AddTokenAsync(tokenEntity);

            return new AuthTokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task Logout(string token)
        {
            await _tokenService.RevokeToken(token); // Xóa token khỏi danh sách hợp lệ
        }


        // Các hàm phụ trợ để xác thực và hash mật khẩu
        private bool VerifyPassword(string password, string passwordHash)
        {
            // Hash lại mật khẩu nhập vào để so sánh với mật khẩu hash đã lưu
            var hashedPassword = HashPassword(password);

            // So sánh mật khẩu đã mã hóa với mật khẩu đã mã hóa lưu trữ
            return hashedPassword == passwordHash;
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
