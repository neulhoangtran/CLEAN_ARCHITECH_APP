namespace App.Domain.Entities
{
    public class UserProfile: BaseEntity
    {
        public int UserID { get; set; } // Khóa ngoại liên kết với bảng Users
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        // Navigation property - Liên kết 1-1 với User
        public User User { get; set; }

        public UserProfile(string fullName, string address, string phoneNumber)
        {
            FullName = fullName;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        // Phương thức cập nhật thông tin cá nhân
        public void UpdateProfile(string fullName, string address, string phoneNumber, DateTime DateOfBirth)
        {
            FullName = fullName;
            Address = address;
            PhoneNumber = phoneNumber;
            DateOfBirth = DateOfBirth;
        }
    }
}
