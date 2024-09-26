using System;

namespace App.Domain.Entities
{
    public class Token : BaseEntity // Kế thừa BaseEntity
    {
        public int UserId { get; set; } // Khóa ngoại liên kết với bảng User
        public string TokenValue { get; set; } // Giá trị token
        public DateTime Expiration { get; set; } // Thời gian hết hạn của token
        public string TokenType { get; set; } // Loại token (Access, Refresh, ResetPassword)

        // Navigation property
        public User User { get; set; } // Quan hệ 1-n với bảng User
    }
}
