﻿namespace App.Domain.Entities
{
    public class User: BaseEntity
    {
        public string Username { get; set; }
        public string EmployeeId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserStatus Status { get; set; }

        // Navigation properties
        public ICollection<UserRole> UserRoles { get; set; } // Quan hệ với bảng UserRole
        public ICollection<Token> Tokens { get; set; } // Quan hệ với bảng Token
        public UserProfile UserProfile { get; set; } // Quan hệ 1-1 với UserProfile

        // Constructor cho đăng ký người dùng mới
        public User() { }
        public User(string username,string EmployeeId, string email, string passwordHash, UserStatus status)
        {
            //UserId = Guid.NewGuid();
            Username = username;
            EmployeeId = EmployeeId;
            PasswordHash = passwordHash;
            Email = email;
            Status = UserStatus.Active;
        }

        // Phương thức cập nhật thông tin người dùng
        public void UpdateUserProfile(UserProfile userProfile)
        {
            UserProfile = userProfile;
        }

        // Phương thức vô hiệu hóa người dùng
        public void Deactivate()
        {
            Status = UserStatus.Inactive;
        }

        // Phương thức cập nhật mật khẩu
        public void UpdatePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }
    }
}