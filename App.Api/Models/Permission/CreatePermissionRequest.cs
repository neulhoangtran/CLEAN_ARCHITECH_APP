using System.ComponentModel.DataAnnotations;

namespace App.Api.Models.Permission
{
    public class CreatePermissionRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }
}
