using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Starship2
    {
        [Required]
        [StringLength(10, ErrorMessage = "Id is too long.")]
        public string? Id { get; set; }
    }
}
