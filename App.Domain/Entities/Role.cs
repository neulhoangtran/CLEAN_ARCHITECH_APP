namespace App.Domain.Entities
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }
        //public string Description { get; private set; }

        // Navigation property - Liên kết 1-n với bảng UserRole
        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
        public Role() { }
        public Role(string name)
        {
            Name = name;
        }
    }
}
