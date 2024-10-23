namespace App.Domain.Entities
{
    public class RolePermission // Không kế thừa BaseEntity vì dùng Composite Key
    {
        public int RoleID { get; set; } // Khóa ngoại liên kết với bảng Role
        public int PermissionID { get; set; } // Khóa ngoại liên kết với bảng Permission

        // Navigation properties
        public Role Role { get; set; } // Quan hệ 1-n với bảng Role
        public Permission Permission { get; set; } // Quan hệ 1-n với bảng Permission
    }
}
