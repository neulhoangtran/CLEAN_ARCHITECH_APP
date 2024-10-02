using App.Domain.Entities;

namespace App.Api.Models
{
    public class UpdateUserRequest
    {
        public string? Username { get; set; }
        public string? EmployeeId { get; set; }
        public string? Email { get; set; }
        public UserStatus? Status { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Role { get; set; }
    }
}
