using System.Collections.Generic;

namespace App.Domain.Entities
{
    public class Permission : BaseEntity // Kế thừa BaseEntity
    {
        public string Name { get; set; } // Tên quyền hạn (ví dụ: ViewUsers, EditUsers)
        public string Description { get; set; } // Mô tả chi tiết quyền hạn
                                                // Navigation property
                                                // Foreign key for PermissionCategory

        public int PermissionCategoryID { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        public PermissionCategory PermissionCategory { get; set; }
        public Permission() { }
    }
}
