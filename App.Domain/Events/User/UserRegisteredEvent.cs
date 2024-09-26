namespace App.Domain.Events.User
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
