using System;

namespace App.Domain.Entities
{
    public class Token : BaseEntity // Kế thừa BaseEntity
    {
        public int UserID { get; set; } // Khóa ngoại liên kết với bảng User
        public string TokenValue { get; set; } // Giá trị token
        public string TokenType { get; set; } // Loại token (AccessToken, RefreshToken, ResetPassword)
        public DateTime Expiration { get; set; } // Thời gian hết hạn của token

        // Navigation property
        public User User { get; set; } // Quan hệ 1-n với bảng User

        public Token() { }
        public Token(int userId, string tokenValue, string tokenType, DateTime expiration)
        {
            UserID = userId;
            TokenValue = tokenValue;
            TokenType = tokenType;
            Expiration = expiration;
        }
    }
}
