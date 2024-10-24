namespace BlazorWebAssembly.Models.Permission
{
    public class PermissionModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int PermissionCategoryID { get; set; }
        public bool IsChecked { get; set; } = false;
    }
}
