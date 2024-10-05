namespace WebApp.Models
{
    // Mô hình dữ liệu phản hồi từ API
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TokenModel Token { get; set; } // Token là đối tượng chứa accessToken và refreshToken

        public class TokenModel
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }
    }
}
