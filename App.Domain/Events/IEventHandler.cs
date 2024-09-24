using System.Threading.Tasks;

namespace App.Domain.Events
{
    public interface IEventHandler<TEvent>
    {
        Task Handle(TEvent @event); // Đảm bảo phương thức trả về Task
    }
}
