namespace App.Application.DTOs
{
    public class UserDto: BaseDTO
    {
        public string Username { get; set; }
        public string EmployeeId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
