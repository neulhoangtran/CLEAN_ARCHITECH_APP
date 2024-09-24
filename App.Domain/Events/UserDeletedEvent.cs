namespace App.Domain.Events
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
