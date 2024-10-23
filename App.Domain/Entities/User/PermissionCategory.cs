namespace App.Domain.Entities
{
    public class PermissionCategory:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; } = 1;
        // Navigation property for one-to-many relationship
        public ICollection<Permission> Permissions { get; set; }

        public PermissionCategory()
        {
            Permissions = new List<Permission>();
        }
    }
}
