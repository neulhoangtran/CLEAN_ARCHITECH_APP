using System.ComponentModel.DataAnnotations;

namespace BlazorWebAssembly.Models.User
{
    public class UserModel
    {

        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Role { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Department { get; set; }

        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
