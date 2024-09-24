namespace App.Api.Models
{
    public class RegisterUserRequest
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; } // Mã hóa mật khẩu trong service
        public string? Role { get; set; }
    }
}
