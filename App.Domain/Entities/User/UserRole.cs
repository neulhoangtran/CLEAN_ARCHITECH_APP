namespace App.Domain.Entities
{
    public class UserRole // Không kế thừa BaseEntity vì dùng Composite Key
    {
        public int UserID { get; set; } // Khóa ngoại liên kết với bảng User
        public User User { get; set; } // Quan hệ 1-n với bảng User

        public int RoleID { get; set; } // Khóa ngoại liên kết với bảng Role
        public Role Role { get; set; } // Quan hệ 1-n với bảng Role

        public UserRole() { }
        public UserRole(int UserId, int role) { 
            UserID = UserId;
            RoleID = role;
        }

    }
}
