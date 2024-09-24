namespace App.Domain.Events
{
    public class UserRegisteredEvent
    {
        public int Id { get; }
        public string Username { get; }
        public string Email { get; }

        public UserRegisteredEvent(int UserId, string username, string email)
        {
            Id = UserId;
            Username = username;
            Email = email;
        }
    }
}
