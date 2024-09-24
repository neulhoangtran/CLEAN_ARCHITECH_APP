namespace App.Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }

        public UserProfile(string fullName, string address, string phoneNumber)
        {
            FullName = fullName;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        // Phương thức cập nhật thông tin cá nhân
        public void UpdateProfile(string fullName, string address, string phoneNumber)
        {
            FullName = fullName;
            Address = address;
            PhoneNumber = phoneNumber;
        }
    }
}
