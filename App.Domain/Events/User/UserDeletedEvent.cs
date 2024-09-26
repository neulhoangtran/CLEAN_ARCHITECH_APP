namespace App.Domain.Events.User
{
    public class UserDeletedEvent
    {
        public int Id { get; }

        public UserDeletedEvent(int userId)
        {
            Id = userId;
        }
    }
}
