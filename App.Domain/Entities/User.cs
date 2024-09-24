namespace App.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; private set; }
        public string Role { get; set; }
        public bool IsActive { get; private set; }
        public UserProfile UserProfile { get; private set; }

        // Constructor cho đăng ký người dùng mới
        public User(string username, string email, string passwordHash, string role)
        {
            //UserId = Guid.NewGuid();
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            IsActive = true;
        }

        // Phương thức cập nhật thông tin người dùng
        public void UpdateUserProfile(UserProfile userProfile)
        {
            UserProfile = userProfile;
        }

        // Phương thức vô hiệu hóa người dùng
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
