using System.Threading.Tasks;
using App.Domain.Events;
using App.Application.Interfaces;
using App.Domain.Events.User;

namespace App.Application.Handlers
{
    public class UserDeletedEventHandler : IEventHandler<UserDeletedEvent>
    {
        private readonly IUserService _userService;

        public UserDeletedEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        // Sửa đổi phương thức Handle để trả về Task và sử dụng async
        public async Task Handle(UserDeletedEvent @event)
        {
            // Logic xử lý khi người dùng bị xóa
            await _userService.DeleteUserAsync(@event.Id); // Gọi phương thức DeleteUser một cách bất đồng bộ
        }
    }
}
