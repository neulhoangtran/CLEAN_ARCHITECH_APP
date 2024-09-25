namespace App.Application.DTOs
{
    public abstract class BaseDTO
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
