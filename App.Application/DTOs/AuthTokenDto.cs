namespace App.Application.DTOs
{
    public class AuthTokenDto
    {
        // Token xác thực
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        // Thời gian hết hạn của token
        public DateTime Expiration { get; set; }
    }
}
