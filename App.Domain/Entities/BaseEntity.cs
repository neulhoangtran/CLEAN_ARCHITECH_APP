namespace App.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int ID { get; set; } // Khóa chính
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Mặc định là thời gian hiện tại
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Mặc định là thời gian hiện tại
    }
}
