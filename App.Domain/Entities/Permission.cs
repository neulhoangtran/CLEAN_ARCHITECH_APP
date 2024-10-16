using System.Collections.Generic;

namespace App.Domain.Entities
{
    public class Permission : BaseEntity // Kế thừa BaseEntity
    {
        public string PermissionName { get; set; } // Tên quyền hạn (ví dụ: ViewUsers, EditUsers)
        public string Description { get; set; } // Mô tả chi tiết quyền hạn
        public string Group { get; set; } // Mô tả chi tiết quyền hạn

        // Navigation property
        public ICollection<RolePermission> RolePermissions { get; set; } // Quan hệ n-n với bảng Role thông qua RolePermission
    }
}
