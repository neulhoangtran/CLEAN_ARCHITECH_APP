using System.Collections.Generic;

namespace App.Application.DTOs
{
    public class PermissionCategoryDto
    {
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public int CategoryOrder { get; set; }
        public List<PermissionDto>? Permissions { get; set; }
    }
}
