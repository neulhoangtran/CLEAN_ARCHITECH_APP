namespace BlazorWebAssembly.Models.Permission
{
    public class PermissionGroupModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public List<PermissionModel> Permissions { get; set; } = new List<PermissionModel>();
    }
}
