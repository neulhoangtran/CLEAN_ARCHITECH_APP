namespace App.Domain.Entities
{
    public enum UserStatus
    {
        Active = 1,    // Người dùng hoạt động
        Inactive = 2,   // Người dùng bị khóa
        Blocked = 3   // Người dùng không hoạt động
    }
}
