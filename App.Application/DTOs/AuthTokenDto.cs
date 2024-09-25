namespace App.Application.DTOs
{
    public class AuthTokenDto
    {
        // Token xác thực
        public string Token { get; set; }

        // Thời gian hết hạn của token
        public DateTime Expiration { get; set; }
    }
}
